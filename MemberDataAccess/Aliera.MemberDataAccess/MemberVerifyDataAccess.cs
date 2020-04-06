using Aliera.BusinessObjects.Audit;
using Aliera.BusinessObjects.Member;
using Aliera.DatabaseEntities.Models;
using Aliera.MemberDataAccess.Mapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;
using Aliera.Utilities.Constants;
using Aliera.Utilities.Enumerations;

namespace Aliera.MemberDataAccess
{
    public class MemberVerifyDataAccess : IMemberVerifyDataAccess
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _memoryCache;

        public MemberVerifyDataAccess(IUnitOfWork unitOfWork, IMemoryCache memoryCache)
        {
            _unitOfWork = unitOfWork;
            _memoryCache = memoryCache;
        }

        /// <summary>
        /// Determines whether [is member valid] [the specified member reg bo].
        /// </summary>
        /// <param name="memberRegBO">The member reg bo.</param>
        /// <param name="auditLogBO">The audit log bo.</param>
        /// <returns></returns>
        public async Task<Tuple<int, string, string>> IsMemberValid(MemberRegistrationBO memberRegBO, AuditLogBO auditLogBO)
        {
            var statusCode = MemberRegistrationVerification.InvalidMemberId;
            var externalId = memberRegBO.ExternalId;
            var memberVerificationStatus = await VerifyMemberDetails(memberRegBO, statusCode, externalId);

            await AuditMapper.MapAuditAndQueue(auditLogBO, AuditAction.Select, null);
            return Tuple.Create((int)memberVerificationStatus.Item1, memberVerificationStatus.Item2, memberVerificationStatus.Item3);
        }

        /// <summary>
        /// Verify if username si available through already used username
        /// </summary>
        /// <param name="username"></param>
        /// <param name="auditLogBO"></param>
        /// <returns>Returns True if username is available</returns>
        public async Task<bool> IsUsernameAvailable(string username, AuditLogBO auditLogBO)
        {
            var userRepo = _unitOfWork.GetRepository<Users>();
            //await AuditMapper.AuditLogging(auditLogBO, null, AuditAction.Select, null);
            return await userRepo.GetFirstOrDefaultAsync(a => a, predicate: b => b.UserName == username) != null;
        }

        /// <summary>
        /// Validates the otp.
        /// </summary>
        /// <param name="externalId">The external identifier.</param>
        /// <param name="otp">The otp.</param>
        /// <param name="auditLogBO">The audit log bo.</param>
        /// <returns></returns>
        public async Task<bool> ValidateOtp(string externalId, int otp, AuditLogBO auditLogBO)
        {
            var response = false;
            var memberDetails = await _unitOfWork.GetRepository<Member>().GetFirstOrDefaultAsync(predicate: m => m.ExternalId == externalId);

            if (memberDetails != null)
            {
                var otpValue = _memoryCache.Get(memberDetails.MemberId);
                if (otpValue != null)
                {
                    var currentTime = DateTime.Now;
                    var otpGeneratedTime = (DateTime)_memoryCache.Get(memberDetails.MemberId + MemberConstants.GeneratedTime);
                    var differenceInMinutes = currentTime.Subtract(otpGeneratedTime).TotalMinutes;
                    if ((int)otpValue == otp && differenceInMinutes < 3.0)
                        response = true;

                    //await AuditMapper.AuditLogging(auditLogBO, null, AuditAction.Select, null);
                }
            }
            return response;
        }

        /// <summary>
        /// Verifies the member details.
        /// </summary>
        /// <param name="memberRegBO">The member reg bo.</param>
        /// <param name="statusCode">The status code.</param>
        /// <param name="externalId">The external identifier.</param>
        /// <returns></returns>
        private async Task<Tuple<MemberRegistrationVerification, string, string>> VerifyMemberDetails(MemberRegistrationBO memberRegBO, MemberRegistrationVerification statusCode, string externalId)
        {
            DateTime? dateOfBirth = memberRegBO.Dob;
            var memberRepo = _unitOfWork.GetRepository<Member>();
            var memberDependentRepo = _unitOfWork.GetRepository<MemberDependent>();
            var memberDetailsRepo = _unitOfWork.GetRepository<MemberDetail>();
            MemberDependent memberDependentDetails;
            var phoneNUmber = string.Empty;
            var emailId = string.Empty;

            var memDetails = await memberRepo.GetFirstOrDefaultAsync(a => a, predicate: b => b.Admin123Id == externalId);
            if (memDetails == null)
            {
                memDetails = await memberRepo.GetFirstOrDefaultAsync(a => a, predicate: b => b.ExternalId == externalId);
                memberDependentDetails = await memberDependentRepo.GetFirstOrDefaultAsync(a => a, predicate: b => b.ExternalId == externalId);
            }
            else
                memberDependentDetails = await memberDependentRepo.GetFirstOrDefaultAsync(a => a, predicate: b => b.Admin123Id == externalId);

            if (memDetails != null)
            {
                statusCode = memDetails.UserId != null ? MemberRegistrationVerification.ExistingMember : MemberRegistrationVerification.ValidMemberId;

                //if valid memberId
                if (statusCode != MemberRegistrationVerification.ExistingMember)
                {
                    var memAddress = await _unitOfWork.GetRepository<MemberAddress>().GetFirstOrDefaultAsync(a => a,
                        predicate: b => b.MemberId == memDetails.MemberId);
                    var memberDetails = await _unitOfWork.GetRepository<MemberDetail>().GetFirstOrDefaultAsync(a => a,
                        predicate: b => b.MemberDetailId == memDetails.MemberDetailId);

                    if (memAddress != null && memberDetails != null)
                    {
                        var memberDetailsResponse = await memberDetailsRepo.GetFirstOrDefaultAsync(a => a,
                            predicate: b => b.MemberDetailId == memDetails.MemberDetailId && b.DateOfBirth.Date == dateOfBirth.Value.Date
                            && b.FirstName.ToLower().Trim() == memberRegBO.FirstName.ToLower().Trim() &&
                            b.LastName.ToLower().Trim() == memberRegBO.LastName.ToLower().Trim()
                            && memAddress.ZipCode == memberRegBO.ZipCode);

                        statusCode = memberDetailsResponse != null ? MemberRegistrationVerification.Verified : MemberRegistrationVerification.InvalidDetails;

                        if (statusCode.Equals(MemberRegistrationVerification.Verified))
                        {
                            //phoneNumber or email null check and validation if value is not null
                            var errorStatusCode = memberDetails.PhoneNumber == null && memberDetails.EmailId == null ?
                                MemberRegistrationVerification.EmailIdAndPhoneNumberAreEmpty :
                                (memberDetails.EmailId == null ? MemberRegistrationVerification.EmailIdEmpty :
                                (memberDetails.PhoneNumber == null ? MemberRegistrationVerification.PhoneNumberEmpty : statusCode));

                            phoneNUmber = !string.IsNullOrEmpty(memberDetailsResponse.PhoneNumber) ?
                                memberDetailsResponse.PhoneNumber.Substring(7, 3).PadLeft(10, '*') : null;
                            emailId = !string.IsNullOrEmpty(memberDetailsResponse.EmailId) ?
                                (memberDetailsResponse.EmailId.Split('@')[0].Substring(0, 2)
                                + new string('*', memberDetailsResponse.EmailId.Split('@')[0].Length - 2)
                                + "@"
                                + memberDetailsResponse.EmailId.Split('@')[1]) : null;

                            if (errorStatusCode != MemberRegistrationVerification.EmailIdAndPhoneNumberAreEmpty)
                            {
                                statusCode = (memberDetailsResponse.EmailId.ToLower() != memberRegBO.Email.ToLower()
                                    && memberDetailsResponse.PhoneNumber != memberRegBO.PhoneNumber) ?
                                MemberRegistrationVerification.InvalidEmailIdAndPhoneNumber :
                                (memberDetailsResponse.EmailId.ToLower() != memberRegBO.Email.ToLower() ? MemberRegistrationVerification.InvalidEmailId :
                                 (memberDetailsResponse.PhoneNumber != memberRegBO.PhoneNumber ? MemberRegistrationVerification.InvalidPhoneNumber :
                                 MemberRegistrationVerification.Verified));

                                statusCode = (errorStatusCode == MemberRegistrationVerification.EmailIdEmpty
                                    && statusCode == MemberRegistrationVerification.InvalidPhoneNumber) ?
                                    MemberRegistrationVerification.EmailIdEmptyAndInvalidPhoneNumber :
                                    ((errorStatusCode == MemberRegistrationVerification.PhoneNumberEmpty
                                      && statusCode == MemberRegistrationVerification.InvalidEmailId) ?
                                       MemberRegistrationVerification.PhoneNumberEmptyAndInvalidEmailId : statusCode);
                            }
                        }
                    }
                    else
                        statusCode = MemberRegistrationVerification.InvalidDetails;
                }
            }
            else if (memberDependentDetails != null)
            {
                statusCode = memberDependentDetails.UserId != null ? MemberRegistrationVerification.ExistingMember : MemberRegistrationVerification.ValidMemberId;

                //if valid memberId
                if (statusCode != MemberRegistrationVerification.ExistingMember)
                {
                    var memAddress = await _unitOfWork.GetRepository<MemberAddress>().GetFirstOrDefaultAsync(a => a,
                        predicate: b => b.MemberId == memberDependentDetails.MemberId);
                    var dependentDetails = await _unitOfWork.GetRepository<MemberDetail>().GetFirstOrDefaultAsync(a => a,
                        predicate: b => b.MemberDetailId == memberDependentDetails.MemberDetailId);
                    if (memAddress != null)
                    {
                        var memberDependentDetailsResponse = await memberDetailsRepo.GetFirstOrDefaultAsync(a => a,
                             predicate: b => b.DateOfBirth.Date == dateOfBirth.Value.Date
                             && b.MemberDetailId == memberDependentDetails.MemberDetailId && b.FirstName == memberRegBO.FirstName
                             && b.LastName == memberRegBO.LastName && memAddress.ZipCode == memberRegBO.ZipCode);

                        statusCode = memberDependentDetailsResponse != null ? MemberRegistrationVerification.Verified :
                        MemberRegistrationVerification.InvalidDetails;

                        if (statusCode.Equals(MemberRegistrationVerification.Verified))
                        {
                            //phoneNumber or email null check and validation if value is not null
                            var errorStatusCode = dependentDetails.PhoneNumber == null && dependentDetails.EmailId == null ?
                                MemberRegistrationVerification.EmailIdAndPhoneNumberAreEmpty :
                                (dependentDetails.EmailId == null ? MemberRegistrationVerification.EmailIdEmpty :
                                (dependentDetails.PhoneNumber == null ? MemberRegistrationVerification.PhoneNumberEmpty : statusCode));

                            phoneNUmber = memberDependentDetailsResponse.PhoneNumber;
                            emailId = memberDependentDetailsResponse.EmailId;

                            if (errorStatusCode != MemberRegistrationVerification.EmailIdAndPhoneNumberAreEmpty)
                            {
                                statusCode = (memberDependentDetailsResponse.EmailId.ToLower() != memberRegBO.Email.ToLower()
                                    && memberDependentDetailsResponse.PhoneNumber != memberRegBO.PhoneNumber) ?
                                MemberRegistrationVerification.InvalidEmailIdAndPhoneNumber :
                                (memberDependentDetailsResponse.EmailId.ToLower() != memberRegBO.Email.ToLower() ?
                                MemberRegistrationVerification.InvalidEmailId : (memberDependentDetailsResponse.PhoneNumber != memberRegBO.PhoneNumber ?
                                MemberRegistrationVerification.InvalidPhoneNumber : MemberRegistrationVerification.Verified));

                                statusCode = (errorStatusCode == MemberRegistrationVerification.EmailIdEmpty
                                    && statusCode == MemberRegistrationVerification.InvalidPhoneNumber) ?
                                    MemberRegistrationVerification.EmailIdEmptyAndInvalidPhoneNumber :
                                    ((errorStatusCode == MemberRegistrationVerification.PhoneNumberEmpty
                                      && statusCode == MemberRegistrationVerification.InvalidEmailId) ?
                                       MemberRegistrationVerification.PhoneNumberEmptyAndInvalidEmailId : statusCode);
                            }
                        }
                    }
                    else
                        statusCode = MemberRegistrationVerification.InvalidDetails;
                }
            }

            return Tuple.Create(statusCode, phoneNUmber, emailId);
        }
    }
}