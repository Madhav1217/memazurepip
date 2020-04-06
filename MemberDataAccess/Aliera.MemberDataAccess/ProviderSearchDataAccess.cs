using Aliera.BusinessObjects.Audit;
using Aliera.BusinessObjects.Member;
using Aliera.DatabaseEntities.Models;
using Aliera.MemberDataAccess.Mapper;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Aliera.Utilities.Constants;
using Aliera.Utilities.Enumerations;

namespace Aliera.MemberDataAccess
{
    public class ProviderSearchDataAccess : IProviderSearchDataAccess
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProviderSearchDataAccess(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Gets the provider search URL.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="planType">Type of the plan.</param>
        /// <param name="auditLogBO">The audit log bo.</param>
        /// <returns></returns>
        public async Task<ProviderSearchBO> GetProviderSearchUrl(long userId, string planType, AuditLogBO auditLogBO)
        {
            var providerSearchBO = new ProviderSearchBO();
            var planTypeId = 0;
            if (userId != 0)
            {
                var member = await _unitOfWork.GetRepository<Member>().GetFirstOrDefaultAsync(a => a, m => m.UserId == userId);
                if (member != null)
                {
                    if (planType.ToLower().Equals(MemberConstants.MedicalPlan))
                        planTypeId = 1;
                    else if (planType.ToLower().Equals(MemberConstants.DentalPlan) || planType.ToLower().Equals(MemberConstants.VisionPlan) || planType.ToLower().Equals(MemberConstants.DentalVisionPlan))
                        planTypeId = 2;
                    providerSearchBO = await GetProviderSearchUrl(planType, providerSearchBO, planTypeId, member);

                    //Log audit
                    //await AuditMapper.AuditLogging(auditLogBO, userId, AuditAction.Select, member);
                }
            }

            return providerSearchBO;
        }

        /// <summary>
        /// Gets the provider search URL.
        /// </summary>
        /// <param name="planType">Type of the plan.</param>
        /// <param name="providerSearchBO">The provider search bo.</param>
        /// <param name="planTypeId">The plan type identifier.</param>
        /// <param name="member">The member.</param>
        /// <returns></returns>
        private async Task<ProviderSearchBO> GetProviderSearchUrl(string planType, ProviderSearchBO providerSearchBO, int planTypeId, Member member)
        {
            var memberSubscription = await _unitOfWork.GetRepository<MemberSubscription>().GetFirstOrDefaultAsync(a => a,
                                ms => ms.MemberId == member.MemberId);

            var memberSubscribedPlan = await _unitOfWork.GetRepository<MemberSubscribedPlan>().GetPagedListAsync(a => a,
            msp => msp.MemberSubscriptionId == memberSubscription.MemberSubscriptionId && msp.PlanType == planTypeId,
            pageIndex: BrokerConstants.PAGE_INDEX, pageSize: BrokerConstants.PAGE_SIZE);

            var planIds = memberSubscribedPlan.Items.Select(p => p.PlanId).ToList();

            var plans = await _unitOfWork.GetRepository<Plan>().GetPagedListAsync(a => a,
            predicate: p => planIds.Contains(p.PlanId),
            pageIndex: BrokerConstants.PAGE_INDEX, pageSize: BrokerConstants.PAGE_SIZE);

            var productIds = plans?.Items.Select(p => p.ProductId).ToList();
            if (productIds != null)
            {
                var productProviderNetworks = await _unitOfWork.GetRepository<ProductProviderNetwork>().GetPagedListAsync(a => a,
                    predicate: ppn => productIds.Contains(ppn.ProductId),
                    pageIndex: BrokerConstants.PAGE_INDEX, pageSize: BrokerConstants.PAGE_SIZE);
                if (productProviderNetworks != null)
                {
                    var networkIds = productProviderNetworks.Items.Select(item => item.ProviderNetworkId).ToList();
                    var planNames = plans.Items.Select(p => p.Name).FirstOrDefault();

                    var providerNetworks = await _unitOfWork.GetRepository<ProviderNetwork>().GetPagedListAsync(a => a,
                        predicate: pn => networkIds.Contains(pn.ProviderNetworkId),
                        pageIndex: BrokerConstants.PAGE_INDEX, pageSize: BrokerConstants.PAGE_SIZE);

                    if (providerNetworks != null)
                    {
                        if (planNames.ToLower().Contains(MemberConstants.TrinityDentalVision) && planType.ToLower().Equals(MemberConstants.DentalPlan))
                        {
                            providerSearchBO = providerNetworks.Items.Where(i => i.ProviderNetworkId == 3).Select(item => new ProviderSearchBO
                            {
                                NetworkName = item.Name,
                                URL = item.Url
                            }).FirstOrDefault();
                        }
                        else if (planNames.ToLower().Contains(MemberConstants.TrinityDentalVision) && planType.ToLower().Equals(MemberConstants.VisionPlan))
                        {
                            providerSearchBO = providerNetworks.Items.Where(i => i.ProviderNetworkId == 4).Select(item => new ProviderSearchBO
                            {
                                NetworkName = item.Name,
                                URL = item.Url
                            }).FirstOrDefault();
                        }
                        else
                        {
                            providerSearchBO = providerNetworks.Items.Select(item => new ProviderSearchBO
                            {
                                NetworkName = item.Name,
                                URL = item.Url
                            }).FirstOrDefault();
                        }
                    }
                }
            }

            return providerSearchBO;
        }
    }
}