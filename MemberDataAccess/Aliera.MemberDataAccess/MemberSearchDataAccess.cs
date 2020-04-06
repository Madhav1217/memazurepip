using Aliera.BusinessObjects.Member;
using Aliera.DatabaseEntities.Models;
using Aliera.DatabaseEntities.Models.Auth;
using Aliera.Utilities;
using Aliera.Utilities.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aliera.MemberDataAccess
{
    public class MemberSearchDataAccess : IMemberSearchDataAccess
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUnitOfWork<AuthContext> _authUnitOfWork;
        private readonly IOptions<AppSettings> _appSettings;

        public MemberSearchDataAccess(IUnitOfWork unitOfWork, IUnitOfWork<AuthContext> authUnitOfWork, IOptions<AppSettings> appSettings)
        {
            _unitOfWork = unitOfWork;
            _authUnitOfWork = authUnitOfWork;
            _appSettings = appSettings;
        }

        /// <summary>
        /// Gets the members by search.
        /// </summary>
        /// <param name="memberSearchBO">The member search bo.</param>
        /// <returns></returns>
        public async Task<List<MemberDataBO>> GetMembersBySearch(MemberSearchBO memberSearchBO)
        {
            var response = new List<MemberDataBO>();
            if (memberSearchBO != null)
            {
                var memberRepo = _unitOfWork.GetRepository<Member>();
                var states = await _unitOfWork.GetRepository<State>().GetPagedListAsync(a => a, pageIndex:
                    BrokerConstants.PAGE_INDEX, pageSize: BrokerConstants.PAGE_SIZE);
                var members = await memberRepo.GetPagedListAsync(a => a,
                    predicate: (member => !string.IsNullOrEmpty(memberSearchBO.MemberId)
                                    ? member.ExternalId.Trim().ToLower() == memberSearchBO.MemberId.Trim().ToLower() : true),
                    include: src => src
                            .Include(m => m.MemberDetail)
                            .Where(m => !string.IsNullOrEmpty(memberSearchBO.EmailId)
                                    ? memberSearchBO.EmailId.Trim().Equals(m.MemberDetail.EmailId) : true)
                                .Where(m => !string.IsNullOrEmpty(memberSearchBO.FirstName)
                                    ? m.MemberDetail.FirstName.ToLower().Contains(memberSearchBO.FirstName.Trim().ToLower()) : true)
                                .Where(m => !string.IsNullOrEmpty(memberSearchBO.LastName)
                                    ? m.MemberDetail.LastName.ToLower().Contains(memberSearchBO.LastName.Trim().ToLower()) : true)
                                .Where(m => !string.IsNullOrEmpty(memberSearchBO.PhoneNumber)
                                    ? m.MemberDetail.PhoneNumber.Contains(memberSearchBO.PhoneNumber.Trim()) : true)
                            .Include(m => m.MemberAddress)
                             .Where(m => memberSearchBO.ZipCode != 0 ?
                                    m.MemberAddress.FirstOrDefault(a => a.AddressTypeId == 1).ZipCode.Contains(memberSearchBO.ZipCode.ToString().Trim())
                                    : true)
                            .Include(m => m.MemberSubscription),
                            pageIndex: memberSearchBO.PageNumber,
                            pageSize: memberSearchBO.PageSize).ConfigureAwait(false);

                response = members.Items.Select(m => new MemberDataBO
                {
                    ExternalId = m.ExternalId,
                    UserId = m.UserId,
                    FirstName = m.MemberDetail?.FirstName,
                    LastName = m.MemberDetail?.LastName,
                    BrokerId = m.MemberSubscription.Select(ms => ms.BrokerId).FirstOrDefault(),
                    GroupId = m.MemberSubscription.Select(ms => ms.GroupId).FirstOrDefault(),
                    PhoneNumber = m.MemberDetail.PhoneNumber,
                    Email = m.MemberDetail.EmailId,
                    State = states.Items.FirstOrDefault(x => x.StateCode == m.MemberAddress?.FirstOrDefault(a => a.AddressTypeId == 1)?.StateCode)?.StateName
                }).ToList();
            }
            return response;
        }

        /// <summary>
        /// Gets the user details.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public async Task<MemberDashboardBO> GetUserDetails(long userId)
        {
            var dashboardBO = new MemberDashboardBO();
            var userDetails = await _unitOfWork.GetRepository<Users>().GetFirstOrDefaultAsync(i => i, predicate: u => u.UserId == userId);
            //To fetch last login
            var appUserDetails = await _authUnitOfWork.GetRepository<ApplicationUser>().GetFirstOrDefaultAsync(i => i, predicate: u => u.Id == userId);

            if (userDetails != null && appUserDetails != null)
            {
                dashboardBO.LastLogin = appUserDetails.LastLogin;
                dashboardBO.FirstName = userDetails.FirstName;
                dashboardBO.LastName = userDetails.LastName;
                dashboardBO.SessionIdleTime = _appSettings.Value.SessionIdleTime;
                dashboardBO.SessionTimeOut = _appSettings.Value.SessionTimeOut;
            }

            return dashboardBO;
        }
    }
}