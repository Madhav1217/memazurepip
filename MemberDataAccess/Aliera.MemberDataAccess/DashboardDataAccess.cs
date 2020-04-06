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
using Aliera.Utilities.Helpers;
using Aliera.DatabaseEntities.Models.Auth;
using Microsoft.Extensions.Options;
using Aliera.Utilities;

namespace Aliera.MemberDataAccess
{
    public class DashboardDataAccess : IDashboardDataAccess
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUnitOfWork<AuthContext> _authUnitOfWork;
        private readonly IOptions<AppSettings> _appSettings;

        public DashboardDataAccess(IUnitOfWork unitOfWork, IUnitOfWork<AuthContext> authUnitOfWork, IOptions<AppSettings> appSettings)
        {
            _unitOfWork = unitOfWork;
            _authUnitOfWork = authUnitOfWork;
            _appSettings = appSettings;
        }

        /// <summary>
        /// Gets the member and dependent names.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="auditLogBO">The audit log bo.</param>
        /// <returns></returns>
        public async Task<IEnumerable<MemberDataBO>> GetMemberAndDependentNames(long userId, AuditLogBO auditLogBO)
        {
            IEnumerable<MemberDataBO> memberDataBO = null;
            if (userId > 0)
                memberDataBO = await GetDependentNames(auditLogBO, userId);

            return memberDataBO;
        }

        /// <summary>
        /// Gets the member details for dash board.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="auditLogBO">The audit log bo.</param>
        /// <param name="leftNavPermission">The left nav permission.</param>
        /// <returns></returns>
        public async Task<MemberDashboardBO> GetMemberDetailsForDashBoard(long userId, AuditLogBO auditLogBO, int leftNavPermission)
        {
            var dashboardBO = new MemberDashboardBO();
            var userDetails = await _unitOfWork.GetRepository<Users>().GetFirstOrDefaultAsync(i => i, predicate: u => u.UserId == userId);
            //To fetch last login
            var appUserDetails = await _authUnitOfWork.GetRepository<ApplicationUser>().GetFirstOrDefaultAsync(i => i, predicate: u => u.Id == userId);

            if (userDetails != null && appUserDetails != null)
            {
                var member = await _unitOfWork.GetRepository<Member>()
                 .GetFirstOrDefaultAsync(i => i, predicate: m => m.UserId == userId, include: i => i
                 .Include(a => a.MemberDetail)
                 .Include(a => a.MemberDependent)
                 .ThenInclude(s => s.MemberDetail)
                 .ThenInclude(a => a.MemberDependentStatusHistory)
                 .Include(s => s.MemberSubscription));

                if (member != null)
                {
                    dashboardBO.LastLogin = appUserDetails.LastLogin;
                    dashboardBO.FirstName = member.MemberDetail.FirstName;
                    dashboardBO.LastName = member.MemberDetail.LastName;
                    if (leftNavPermission == MemberConstants.HasPermission)
                    {
                        dashboardBO.ExternalId = member.ExternalId;

                        dashboardBO.DepandentDetails = member.MemberDependent.Select(memberDependents => new MemberDependentBO
                        {
                            Name = memberDependents.MemberDetail.FirstName + " " + memberDependents.MemberDetail.LastName,
                            Gender = memberDependents.MemberDetail.Gender,
                            Age = new DateTime(DateTime.Now.Subtract(memberDependents.MemberDetail.DateOfBirth).Ticks).Year - 1,
                            AvatarImageId = memberDependents.MemberDetail.AvatarImageId,
                            MemberDetailId = memberDependents.MemberDetail.MemberDetailId,
                            Status = SetStatus(memberDependents.MemberDetail.MemberDependentStatusHistory
                            .FirstOrDefault(x => x.MemberDetailId == memberDependents.MemberDetailId), memberDependents)
                        }).ToList();

                        dashboardBO.DepandentDetails.RemoveAll(dep => dep.Status == (int)DependentStatus.PendingActivation);
                    }
                }
            }

            //await AuditMapper.AuditLogging(auditLogBO, userId, AuditAction.Select, null);
            return dashboardBO;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="auditLogBO"></param>
        /// <returns></returns>
        public async Task<CoveredPlansBO> GetMemberPlanTypes(long userId, AuditLogBO auditLogBO)
        {
            var coveredPlansBO = new CoveredPlansBO();
            if (userId != 0)
            {
                var member = await _unitOfWork.GetRepository<Member>().GetFirstOrDefaultAsync(a => a, m => m.UserId == userId);
                if (member != null)
                {
                    var memberSubscription = await _unitOfWork.GetRepository<MemberSubscription>().GetFirstOrDefaultAsync(a => a,
                    ms => ms.MemberId == member.MemberId);

                    var memberSubscribedPlan = await _unitOfWork.GetRepository<MemberSubscribedPlan>().GetPagedListAsync(a => a,
                    msp => msp.MemberSubscriptionId == memberSubscription.MemberSubscriptionId,
                    pageIndex: BrokerConstants.PAGE_INDEX, pageSize: BrokerConstants.PAGE_SIZE);

                    var planIds = memberSubscribedPlan.Items.Select(p => p.PlanId).ToList();
                    if (planIds != null)
                    {
                        var memberPlans = await _unitOfWork.GetRepository<Plan>().GetPagedListAsync(a => a,
                                predicate: p => planIds.Contains(p.PlanId),
                                pageIndex: BrokerConstants.PAGE_INDEX, pageSize: BrokerConstants.PAGE_SIZE);

                        var productIds = memberPlans.Items.Select(p => p.ProductId).ToList();

                        var products = await _unitOfWork.GetRepository<Product>().GetPagedListAsync(a => a,
                            predicate: prd => productIds.Contains(prd.ProductId),
                            pageIndex: BrokerConstants.PAGE_INDEX, pageSize: BrokerConstants.PAGE_SIZE);

                        var result = memberPlans.Items.Join(products.Items,
                            plan => plan.ProductId,
                            product => product.ProductId,
                            (plan, product) => new { plan.PlanId, plan.Type, product.ProductCode });

                        if (result != null)
                        {
                            foreach (var item in result)
                            {
                                switch (item.Type)
                                {
                                    case 1:
                                        coveredPlansBO.IsMedical = true;
                                        break;

                                    case 2:
                                        switch (item.ProductCode)
                                        {
                                            case 21423:
                                                coveredPlansBO.IsDentalVision = true;
                                                break;

                                            case 21424:
                                                coveredPlansBO.IsDental = true;
                                                break;

                                            case 21425:
                                                coveredPlansBO.IsVision = true;
                                                break;

                                            default:
                                                break;
                                        }
                                        break;

                                    default:
                                        break;
                                }
                            }
                        }
                    }
                }
            }

            //Log audit
            //await AuditMapper.AuditLogging(auditLogBO, userId, AuditAction.Select, null);
            return coveredPlansBO;
        }

        /// <summary>
        /// Gets the security questions by user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="auditLogBO">The audit log bo.</param>
        /// <returns></returns>
        public async Task<IEnumerable<SecurityQuestionAnswersBO>> GetSecurityQuestionsByUserId(long userId, AuditLogBO auditLogBO)
        {
            var securityQAs = new List<SecurityQuestionAnswersBO>();
            var securityQuestionAnswers = await _unitOfWork.GetRepository<UserSecurityQuestionAnswers>().GetPagedListAsync(i => i,
                            predicate: item => item.UserId == userId);
            if (securityQuestionAnswers != null)
            {
                if (securityQuestionAnswers != null)
                {
                    securityQAs.AddRange(securityQuestionAnswers.Items.Select(item => new SecurityQuestionAnswersBO
                    {
                        SecurityQuestionId = item.SecurityQuestionId,
                        Answer = item.Answer,
                        UserId = item.UserId
                    }));
                }
            }

            //await AuditMapper.AuditLogging(auditLogBO, userId, AuditAction.Select, null);
            return securityQAs;
        }

        /// <summary>
        /// Updates the security questions by user identifier.
        /// </summary>
        /// <param name="updatedSecurityQuestions">The updated security questions.</param>
        /// <param name="auditLogBO">The audit log bo.</param>
        /// <returns></returns>
        public async Task<bool> UpdateSecurityQuestionsByUserId(List<SecurityQuestionAnswersBO> updatedSecurityQuestions, AuditLogBO auditLogBO)
        {
            var rows = 0;
            var isUpdated = false;

            if (updatedSecurityQuestions != null)
            {
                var securityQA = await _unitOfWork.GetRepository<UserSecurityQuestionAnswers>().GetPagedListAsync(
                    a => a,
                    predicate: b => b.UserId == updatedSecurityQuestions[0].UserId,
                     pageIndex: 0, pageSize: 3);

                _unitOfWork.GetRepository<UserSecurityQuestionAnswers>().Delete(securityQA.Items);

                var userSecurityQA = updatedSecurityQuestions.Select(qa => new UserSecurityQuestionAnswers
                {
                    UserId = qa.UserId,
                    SecurityQuestionId = qa.SecurityQuestionId,
                    Answer = EncryptionHelper.HashCode(qa.Answer),
                    CreatedOn = DateTime.Now,
                    CreatedBy = qa.UserId,
                    ModifiedOn = DateTime.Now,
                    ModifiedBy = qa.UserId
                });

                await _unitOfWork.GetRepository<UserSecurityQuestionAnswers>().InsertAsync(userSecurityQA);
                rows = _unitOfWork.SaveChanges();
                if (rows > 0)
                    isUpdated = true;
            }

            await AuditMapper.AuditLogging(auditLogBO, updatedSecurityQuestions[0].UserId, AuditAction.Update, updatedSecurityQuestions);
            return isUpdated;
        }

        /// <summary>
        /// Determines whether [is group member] [the specified user identifier].
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="auditLogBO">The audit log bo.</param>
        /// <returns></returns>
        public async Task<Tuple<bool, int, int>> IsGroupMember(long userId, AuditLogBO auditLogBO)
        {
            var member = await _unitOfWork.GetRepository<Member>()
                .GetFirstOrDefaultAsync(predicate: m => m.UserId == userId);
            if (member != null)
            {
                var memberSubscription = _unitOfWork.GetRepository<MemberSubscription>()
                    .GetFirstOrDefaultAsync(predicate: ms => ms.MemberId == member.MemberId);

                //await AuditMapper.AuditLogging(auditLogBO, userId, AuditAction.Select, null);

                return Tuple.Create(memberSubscription != null && memberSubscription.Result?.GroupId != null
                    && memberSubscription.Result?.GroupId > 0 ? true : false, _appSettings.Value.SessionIdleTime, _appSettings.Value.SessionTimeOut);
            }

            return null;
        }

        /// <summary>
        /// Gets the member plan details.
        /// </summary>
        /// <param name="memberId">The member identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="auditLogBO">The audit log bo.</param>
        /// <returns></returns>
        public async Task<MemberDashboardBO> GetMemberPlanDetails(long memberId, long userId, AuditLogBO auditLogBO)
        {
            MemberDashboardBO memberDashboardBO = null;
            var memberSubscription = await _unitOfWork.GetRepository<MemberSubscription>()
                 .GetFirstOrDefaultAsync(i => i, predicate: ms => ms.MemberId == memberId,
                 include: i => i
                 .Include(a => a.MemberSubscribedPlan)
                 .ThenInclude(a => a.Plan)
                 .ThenInclude(a => a.Product)
                 .Include(a => a.Broker)
                 .Include(a => a.Group)
                 .Include(a => a.Member)
                 );

            if (memberSubscription != null)
            {
                var planDetails = new List<MemberPlanBO>();

                memberDashboardBO = new MemberDashboardBO
                {
                    GroupId = memberSubscription.Group?.GroupId,
                    BrokerId = memberSubscription.Broker.BrokerId,
                    BrokerName = memberSubscription.Broker?.FirstName + " " + memberSubscription.Broker?.LastName,
                    GroupName = memberSubscription.Group?.GroupLabel,
                    IsInternalBroker = memberSubscription.Broker?.BrokerTypeId.Value == MemberConstants.CallCenterInternal ||
                                       memberSubscription.Broker?.BrokerTypeId.Value == MemberConstants.CallCenterAliera ? true : false,

                    //To verify whether member enrolled in dental, vision plans
                    PlansCovered = new CoveredPlansBO
                    {
                        IsMedical = true
                    }
                };

                var planIds = memberSubscription.MemberSubscribedPlan.Select(p => Convert.ToInt64(p.PlanId)).ToList();

                var memberPlanStatus = await _unitOfWork.GetRepository<MemberStatusHistory>().GetPagedListAsync(a => a,
                            predicate: msh => planIds.Contains(msh.MemberSubscribedPlanId),
                            pageIndex: BrokerConstants.PAGE_INDEX, pageSize: BrokerConstants.PAGE_SIZE);

                var result = memberSubscription.MemberSubscribedPlan.Join(memberPlanStatus.Items,
                    subscribedPlan => subscribedPlan.MemberSubscribedPlanId,
                    statusHistory => statusHistory.MemberSubscribedPlanId,
                    (subscribedPlan, statusHistory) => new
                    {
                        subscribedPlan.PlanId,
                        statusHistory.InActiveDate,
                        subscribedPlan.NextBillingDate
                    });

                if (planIds.Count > 0)
                {
                    if (result.Any())
                    {
                        planDetails = memberSubscription.MemberSubscribedPlan.Join(result,
                            plan => plan.PlanId,
                            r => r.PlanId, (plan, r) => new MemberPlanBO
                            {
                                MemberExternalId = memberSubscription.Member.ExternalId,
                                MSRA = plan.Plan.Msra,
                                Duration = plan.Plan.CoverageDuration,
                                MaxLimit = plan.Plan.CoverageLimit,
                                CoverageDate = memberSubscription.EffectiveDate,
                                NextBillingDate = r.NextBillingDate,
                                InActiveDate = r.InActiveDate,
                                PlanType = plan.Plan.Type,
                                PlanName = plan.Plan.Name
                            }).ToList();
                    }
                    else
                    {
                        planDetails = memberSubscription.MemberSubscribedPlan.Select(plan => new MemberPlanBO
                        {
                            MemberExternalId = memberSubscription.Member.ExternalId,
                            MSRA = plan.Plan.Msra,
                            Duration = plan.Plan.CoverageDuration,
                            MaxLimit = plan.Plan.CoverageLimit,
                            CoverageDate = memberSubscription.EffectiveDate,
                            NextBillingDate = result.Select(ms => ms.NextBillingDate).FirstOrDefault(),
                            PlanType = plan.Plan.Type,
                            PlanName = plan.Plan.Name
                        }).ToList();
                    }

                    memberDashboardBO.PlanDetails = planDetails;

                    memberDashboardBO.ProductCodes = memberSubscription.MemberSubscribedPlan.Select(a => new
                    {
                        name = a.Plan?.Product?.Name,
                        label = (!string.IsNullOrEmpty(a?.Plan?.Product?.ProductLabel)
                        && (a.Plan.Product.ProductLabel.ToLower().Contains(MemberConstants.DentalPlan)
                        || a.Plan.Product.ProductLabel.ToLower().Contains(MemberConstants.VisionPlan)
                        || a.Plan.Product.ProductLabel.ToLower().Contains(MemberConstants.DentalVisionPlan)))
                        ? a.Plan.Product.ProductLabel : MemberConstants.MedicalPlan,
                    }).ToDictionary(dic => dic.name, dic => dic.label);

                    //await AuditMapper.AuditLogging(auditLogBO, userId, AuditAction.Select, null);
                }
            }

            return memberDashboardBO;
        }

        /// <summary>
        /// Gets the member identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public async Task<string> GetMemberExternalId(long userId)
        {
            if (userId > 0)
            {
                var member = await _unitOfWork.GetRepository<Member>().GetFirstOrDefaultAsync(a => a,
                    predicate: mem => mem.UserId == userId);
                return member?.ExternalId;
            }
            return string.Empty;
        }

        /// <summary>
        /// Gets the dependent names.
        /// </summary>
        /// <param name="auditLogBO">The audit log bo.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        private async Task<IEnumerable<MemberDataBO>> GetDependentNames(AuditLogBO auditLogBO, long userId)
        {
            List<MemberDataBO> memberDetails = null;

            var member = await _unitOfWork.GetRepository<Member>()
                  .GetFirstOrDefaultAsync(i => i, predicate: m => m.UserId == userId, include: i => i
                  .Include(a => a.MemberDetail)
                  .Include(a => a.MemberDependent)
                  .ThenInclude(s => s.MemberDetail)
                  .ThenInclude(a => a.MemberDependentStatusHistory));

            if (member != null)
            {
                memberDetails = new List<MemberDataBO>();

                memberDetails.Add(new MemberDataBO // added primary information
                {
                    UserId = member.UserId,
                    ExternalId = member.ExternalId,
                    FirstName = member.MemberDetail.FirstName,
                    LastName = member.MemberDetail.LastName,
                    FullName = $"{member.MemberDetail.FirstName} {member.MemberDetail.LastName}",
                    MemberType = MemberConstants.Primary,
                    MemberDetailId = member.MemberDetail.MemberDetailId,
                    MemberId = member.MemberId
                });

                // added dependent information
                memberDetails.AddRange(member.MemberDependent.Select(memberDependents => new MemberDataBO
                {
                    UserId = memberDependents.UserId,
                    ExternalId = memberDependents.ExternalId,
                    FirstName = memberDependents.MemberDetail.FirstName,
                    LastName = memberDependents.MemberDetail.LastName,
                    FullName = $"{memberDependents.MemberDetail.FirstName} {memberDependents.MemberDetail.LastName}",
                    MemberType = MemberConstants.Dependent,
                    MemberDetailId = memberDependents.MemberDetail.MemberDetailId,

                    Status = SetStatus(memberDependents.MemberDetail.MemberDependentStatusHistory
                            .FirstOrDefault(x => x.MemberDetailId == memberDependents.MemberDetailId), memberDependents)
                }));

                memberDetails.RemoveAll(dep => dep.Status == (int)DependentStatus.PendingActivation);

                //await AuditMapper.AuditLogging(auditLogBO, userId, AuditAction.Select, null);
            }

            return memberDetails;
        }

        /// <summary>
        /// Statuses the specified member dependent status history.
        /// </summary>
        /// <param name="memberDependentStatusHistory">The member dependent status history.</param>
        /// <param name="memberDependent">The member dependent.</param>
        /// <returns></returns>
        private static int SetStatus(MemberDependentStatusHistory memberDependentStatusHistory, MemberDependent memberDependent)
        {
            var dependentStatus = DependentStatus.NoStatus;

            if (memberDependentStatusHistory != null)
            {
                dependentStatus = memberDependentStatusHistory.InActiveDate != null ?
                    DateTime.UtcNow < memberDependentStatusHistory.InActiveDate ?
                        DependentStatus.PendingInactivation : DependentStatus.Inactive

                    : memberDependentStatusHistory.ActiveDate != null ?
                        DateTime.UtcNow < memberDependentStatusHistory.ActiveDate ?
                            DependentStatus.PendingActivation : (memberDependent.DeactiveDate != null
                                ? DependentStatus.PendingInactivation : DependentStatus.Active)
                        : DependentStatus.PendingActivation;
            }
            else
            {
                dependentStatus = memberDependent.DeactiveDate != null
                    ? DependentStatus.PendingInactivation
                    : (String.IsNullOrWhiteSpace(memberDependent.ExternalId) && memberDependent.DeactiveDate == null ?
                        DependentStatus.PendingActivation : DependentStatus.Active);
            }

            return (int)dependentStatus;
        }
    }
}