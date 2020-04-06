using Aliera.BusinessObjects.Member;
using Aliera.DatabaseEntities.Models;
using Aliera.MemberDataAccess.Mapper;
using Aliera.Utilities;
using Aliera.Utilities.Constants;
using Aliera.Utilities.Enumerations;
using Aliera.Utilities.Notifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aliera.MemberDataAccess
{
    public class IDMemberDataAccess : IIDMemberDataAccess
    {
        private IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;
        private readonly IOptions<AppSettings> _appSettings;

        public IDMemberDataAccess(IUnitOfWork unitOfWork, IEmailService emailService, IOptions<AppSettings> appSettings)
        {
            _unitOfWork = unitOfWork;
            _emailService = emailService;
            _appSettings = appSettings;
        }

        public async Task<IDCardBO> GetIDMemberDetails(long userId, int productCode, string productLabel)
        {
            var memberSubscribedPlansList = await _unitOfWork.GetRepository<MemberSubscribedPlan>().
                GetPagedListAsync(m => m,
                                   predicate: m => userId == m.MemberSubscription.Member.UserId,
                                   include: i => i.Include(m => m.MemberSubscription)
                                                  .Include(m => m.Plan)
                                                        .ThenInclude(m => m.Product)
                                                  .Include(m => m.MemberSubscription.Member)
                                                        .ThenInclude(m => m.MemberDetail)
                                                   .Include(m => m.MemberSubscription.Member.MemberDependent)
                                                        .ThenInclude(m => m.MemberDetail)
                                                    .Include(m => m.MemberSubscription.Member.MemberDependentStatusHistory),
                                   pageIndex: BrokerConstants.PAGE_INDEX, pageSize: BrokerConstants.PAGE_SIZE);

            var member = memberSubscribedPlansList?.Items.Select(m => m.MemberSubscription.Member).FirstOrDefault();
            var planType = memberSubscribedPlansList?.Items.Where(msp => msp.Plan.Product.ProductCode == productCode)
                                    .Select(msp => msp.Plan.Type).FirstOrDefault();
            var outerDepHTML = string.Empty;
            var dependentDetails = member?.MemberDependent?.Select(m => new
            {
                FullName = $"{m.MemberDetail.FirstName} {m.MemberDetail.LastName}",
                Relationship = m.RelationshipId,
                MemberDetailId = m.MemberDetailId
            });
            var dependents = string.Empty;
            if (dependentDetails != null)
            {
                var activeDependentDetails = member?.MemberDependentStatusHistory?.Join(dependentDetails, a => a.MemberDetailId, b => b.MemberDetailId,
                                                                                 (a, b) => new { a.MemberDetailId, a.ActiveDate, a.InActiveDate, b.Relationship, b.FullName });

                var activeDependents = activeDependentDetails.
                                            Where(m => (m.ActiveDate <= DateTime.Now && (m.InActiveDate == null || m.InActiveDate > DateTime.Now))).Distinct()
                                            .OrderBy(m => m.Relationship);
                if(planType == (int)PlanType.Primary)
                {
                     outerDepHTML = "<table style=\"padding-left: 58px;margin-top: -13px;font-size: 10px;\"><tbody>{0}</tbody></table>";

                }
                else
                {
                     outerDepHTML = "<table style=\"padding-left: 110px;margin-top: -13px;font-size: 10px;\"><tbody>{0}</tbody></table>";
                }
                var builderDep = new StringBuilder();

                if (activeDependents != null && activeDependents.Count() > 0)
                {
                    var count = 2;
                    foreach (var item in activeDependents)
                    {
                        builderDep.Append($"<tr><td style=\"line-height: 8px;\">&nbsp;0{count} {item.FullName}</td></tr>");
                        count++;
                    }
                }
                dependents = string.Format(outerDepHTML, builderDep.ToString());
            }

            var productId = memberSubscribedPlansList?.Items.Where(d => d.Plan.Product.ProductCode == productCode).
                                                    Select(d => d.Plan.Product.ProductId).FirstOrDefault();

            var templateDetails = await _unitOfWork.GetRepository<TemplateDetails>().GetPagedListAsync(i => i,
                                    predicate: t => t.ProviderNetwork.ProductProviderNetwork.Any(pp => pp.ProductId.Equals(productId)),
                                    include: i => i.Include(t => t.Template)
                                                   .Include(t => t.ProviderNetwork)
                                                    .ThenInclude(t => t.ProductProviderNetwork),
                                    pageIndex: BrokerConstants.PAGE_INDEX, pageSize: BrokerConstants.PAGE_SIZE);
            var htmlTemplate = string.Empty;
            var provider = 0;
            if (productLabel.ToLower() == MemberConstants.DentalPlan.ToLower())
                provider = (int)ProviderNetworkId.Dental;
            else if (productLabel.ToLower() == MemberConstants.VisionPlan.ToLower())
                provider = (int)ProviderNetworkId.Vision;

            if (templateDetails.TotalCount > 1)
            {
                var productIdList = templateDetails?.Items.SelectMany(t => t.ProductId.Split(',').ToList(), (TemplateDetails, ProductId) =>
                                 new { templateId = TemplateDetails.TemplateId, productId = ProductId, html = TemplateDetails.Html, provider = TemplateDetails.ProviderNetworkId });
                htmlTemplate = productIdList.Where(a => a.productId == productId.ToString() && a.provider == provider).Select(a => a.html).FirstOrDefault();

            }
            else
            {
                htmlTemplate = templateDetails?.Items.FirstOrDefault()?.Html;
            }

            return MemberMapper.Map(memberSubscribedPlansList?.Items, member, dependents, htmlTemplate, productCode);
        }

        /// <summary>
        /// Sends the physical identifier card request email.
        /// </summary>
        /// <param name="memberId">The member identifier.</param>
        /// <param name="memberDetailId">The member detail identifier.</param>
        /// <param name="memberType">Type of the member.</param>
        /// <returns></returns>
        public async Task<bool> SendPhysicalIdCardRequestEmail(long memberId, long memberDetailId, string memberType)
        {
            var response = false;
            if (memberDetailId > 0 && !string.IsNullOrEmpty(memberType))
            {
                var subscriberExternalId = string.Empty;
                var firstName = string.Empty;
                var lastName = string.Empty;
                var subscriberOrDependentExternalId = string.Empty;
                var subscriberOrDependent = string.Empty;

                var member = await _unitOfWork.GetRepository<Member>().GetFirstOrDefaultAsync(a => a,
                    predicate: m => m.MemberId == memberId);

                var memberDetails = await _unitOfWork.GetRepository<MemberDetail>().GetFirstOrDefaultAsync(a => a,
                        predicate: m => m.MemberDetailId == memberDetailId,
                        include: i => i
                        .Include(dependent => dependent.MemberDependent));

                if (member != null && memberDetails != null)
                {
                    subscriberExternalId = member.ExternalId;
                    firstName = memberDetails.FirstName;
                    lastName = memberDetails.LastName;
                    subscriberOrDependentExternalId = memberType.Trim().ToLower().Equals(MemberConstants.Primary) ?
                        member.ExternalId :
                        memberDetails.MemberDependent.Select(m => m.ExternalId).FirstOrDefault();
                    subscriberOrDependent = memberType.Trim().ToLower().Equals(MemberConstants.Primary) ?
                        MemberConstants.Subscriber[0].ToString().ToUpper() + MemberConstants.Subscriber.Substring(1) :
                        MemberConstants.Dependent[0].ToString().ToUpper() + MemberConstants.Dependent.Substring(1);

                    var notificaton = await _unitOfWork.GetRepository<Notification>().GetFirstOrDefaultAsync(predicate: n => n.Type == (int)EmailType.MemberRequestForPhysicalIdCard);
                    if (notificaton != null)
                    {
                        var subject = notificaton.Subject.Replace("{subscriberOrDependentExternalId}", subscriberOrDependentExternalId)
                                      .Replace("{firstname}", firstName).Replace("{lastname}", lastName);
                        var messageBody = notificaton.Content.Replace("{firstname}", firstName).Replace("{lastname}", lastName).
                            Replace("{subscriberExternalId}", subscriberExternalId)
                            .Replace("{subscriberOrDependentExternalId}", subscriberOrDependentExternalId)
                            .Replace("{subscriberOrDependent}", subscriberOrDependent);

                        var emailDetails = MemberMapper.Map(_appSettings.Value.MemberPhysicalIDCradRequestEmail, messageBody, subject, _appSettings, notificaton.From);

                        response = await _emailService.SendEmailAsync(emailDetails);
                    }
                }
            }

            return response;
        }
    }
}