using Aliera.BusinessObjects.Audit;
using Aliera.BusinessObjects.Broker;
using Aliera.BusinessObjects.Member;
using Aliera.DatabaseEntities.Models;
using Aliera.MemberDataAccess.Mapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aliera.Utilities.Constants;
using Aliera.Utilities.Enumerations;

namespace Aliera.MemberDataAccess
{
    public class MasterDataAccess : IMasterDataAccess
    {
        private readonly IUnitOfWork _unitOfWork;

        public MasterDataAccess(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Gets the security questions.
        /// </summary>
        /// <param name="auditLogBO">The audit log bo.</param>
        /// <returns></returns>
        public async Task<IEnumerable<SecurityQuestionsBO>> GetSecurityQuestions(AuditLogBO auditLogBO)
        {
            var securityQuestions = await _unitOfWork.GetRepository<SecurityQuestions>().GetPagedListAsync();
            //await AuditMapper.AuditLogging(auditLogBO, null, AuditAction.Select, null);
            return securityQuestions.Items.Select(securityQuestion => new SecurityQuestionsBO
            {
                SecurityQuestionId = securityQuestion.SecurityQuestionId,
                SecurityQuestion = securityQuestion.SecurityQuestion
            }).ToList();
        }

        /// <summary>
        /// Gets the states.
        /// </summary>
        /// <param name="auditLogBO">The audit log bo.</param>
        /// <returns></returns>
        public async Task<IEnumerable<StateBO>> GetStates(AuditLogBO auditLogBO)
        {
            var states = await _unitOfWork.GetRepository<State>().GetPagedListAsync(a => a,
                pageIndex: BrokerConstants.PAGE_INDEX, pageSize: BrokerConstants.PAGE_SIZE);

            var stateBOs = states.Items.Select(state => new StateBO
            {
                StateName = state.StateName,
                StateCode = state.StateCode,
                TimeZone = state.Timezone
            }).ToList();

            //Log audit for select action on List<StateBO>
            //await AuditMapper.AuditLogging(auditLogBO, null, AuditAction.Select, null);

            return stateBOs;
        }

        /// <summary>
        /// Updates the digital document identifier.
        /// </summary>
        /// <param name="digitalDoc">The digital document.</param>
        /// <param name="auditLogBO">The audit log bo.</param>
        /// <returns></returns>
        /// <exception cref="Exception">Unexpected Case</exception>
        public async Task<bool> UpdateDigitalDocumentId(DigitalDocumentBO digitalDoc, AuditLogBO auditLogBO)
        {
            var response = false;
            if (digitalDoc != null)
            {
                switch (digitalDoc.DocumentType.ToLower())
                {
                    case MemberConstants.ClaimsEOB: //Updating EOB document id in Claims table
                        var claimsRepository = _unitOfWork.GetRepository<Claims>();
                        var claim = await claimsRepository.GetFirstOrDefaultAsync(predicate: c => Convert.ToInt64(c.ClaimsNumber) == digitalDoc.Identifier);
                        if (claim != null)
                        {
                            claim.DocumentId = digitalDoc.DocumentId;
                            claimsRepository.Update(claim);
                        }
                        break;

                    case MemberConstants.ProductGuideBook: //Updating product guidebook id in product table
                        var productRepository = _unitOfWork.GetRepository<Product>();
                        var product = await productRepository.GetFirstOrDefaultAsync(predicate: prd => prd.ProductCode == digitalDoc.Identifier);
                        if (product != null)
                        {
                            product.DocumentId = digitalDoc.DocumentId;
                            productRepository.Update(product);
                        }
                        break;

                    case MemberConstants.DigitalIDCard: //Updating digital id card id in member/member dependent table
                        if (digitalDoc.MemberType.ToLower().Equals(MemberConstants.Self))
                        {
                            var memberRepository = _unitOfWork.GetRepository<Member>();
                            var member = await memberRepository.GetFirstOrDefaultAsync(predicate: m => m.UserId == digitalDoc.Identifier);
                            if (member != null)
                            {
                                member.DigitalIdcardId = digitalDoc.DocumentId;
                                memberRepository.Update(member);
                            }
                        }
                        else if (digitalDoc.MemberType.ToLower().Equals(MemberConstants.Dependent))
                        {
                            var memberDependentRepository = _unitOfWork.GetRepository<MemberDependent>();
                            var memberDependent = await memberDependentRepository.GetFirstOrDefaultAsync(predicate: m => m.MemberDetailId == digitalDoc.Identifier);
                            if (memberDependent != null)
                            {
                                memberDependent.DigitalIdcardId = digitalDoc.DocumentId;
                                memberDependentRepository.Update(memberDependent);
                            }
                        }
                        break;

                    case MemberConstants.AvatarImage:  //Updating avatar image id in member details table
                        var memberDetailRepository = _unitOfWork.GetRepository<MemberDetail>();
                        if (digitalDoc.MemberType.ToLower().Equals(MemberConstants.Self))
                        {
                            var memberRepository = _unitOfWork.GetRepository<Member>();
                            var member = await memberRepository.GetFirstOrDefaultAsync(predicate: m => m.UserId == digitalDoc.Identifier);
                            if (member != null)
                            {
                                var memberDetail = await memberDetailRepository.GetFirstOrDefaultAsync(predicate: md => md.MemberDetailId == member.MemberDetailId);
                                if (memberDetail != null)
                                {
                                    memberDetail.AvatarImageId = digitalDoc.DocumentId;
                                    memberDetailRepository.Update(memberDetail);
                                }
                            }
                        }
                        else if (digitalDoc.MemberType.ToLower().Equals(MemberConstants.Dependent))
                        {
                            var memberDetail = await memberDetailRepository.GetFirstOrDefaultAsync(predicate: md => md.MemberDetailId == digitalDoc.Identifier);
                            if (memberDetail != null)
                            {
                                memberDetail.AvatarImageId = digitalDoc.DocumentId;
                                memberDetailRepository.Update(memberDetail);
                            }
                        }
                        break;

                    default:
                        break;
                }

                var rowsAffected = await _unitOfWork.SaveChangesAsync();
                if (rowsAffected > 0)
                    response = true;

                //Log audit
                await AuditMapper.AuditLogging(auditLogBO, null, AuditAction.Update, digitalDoc);
            }

            return response;
        }
    }
}