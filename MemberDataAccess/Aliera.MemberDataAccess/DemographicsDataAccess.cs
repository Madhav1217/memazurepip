using Aliera.BusinessObjects.Audit;
using Aliera.BusinessObjects.Broker;
using Aliera.BusinessObjects.Member;
using Aliera.DatabaseEntities.Models;
using Aliera.MemberDataAccess.Mapper;
using Aliera.Utilities;
using Aliera.Utilities.Constants;
using Aliera.Utilities.Enumerations;
using Aliera.Utilities.ExtensionMethods;
using Aliera.Utilities.Notifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Aliera.MemberDataAccess
{
    public class DemographicsDataAccess : IDemographicsDataAccess
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;
        private readonly IOptions<AppSettings> _appSettings;

        /// <summary>
        /// constructor for Member Demographic Data Access.
        /// </summary>
        /// <param name="unitOfWork">The unit Of Work.</param>
        /// <param name="emailService"></param>
        /// <param name="notificationSettings"></param>
        public DemographicsDataAccess(IUnitOfWork unitOfWork, IEmailService emailService, IOptions<AppSettings> appSettings)
        {
            _unitOfWork = unitOfWork;
            _appSettings = appSettings;
            _emailService = emailService;
        }

        /// <summary>
        /// Gets the member demographics.
        /// </summary>
        /// <param name="demographicsPermissionBO">The demographics permission bo.</param>
        /// <returns></returns>
        public async Task<MemberDemographicsBO> GetMemberDemographics(DemographicsPermissionBO demographicsPermissionBO)
        {
            //var memberRepo = _unitOfWork.GetRepository<Member>();
            //var memberDependentRepo = _unitOfWork.GetRepository<MemberDependent>();

            var member = await _unitOfWork.GetRepository<Member>().GetFirstOrDefaultAsync(a => a, b => b.UserId == demographicsPermissionBO.UserId);

            var dependents = await _unitOfWork.GetRepository<MemberDependent>().GetPagedListAsync(a => a, predicate: b => b.MemberId == member.MemberId,
                pageIndex: BrokerConstants.PAGE_INDEX, pageSize: BrokerConstants.PAGE_SIZE);

            var memberDependent = dependents.Items.ToList();

            var memberDetails = await _unitOfWork.GetRepository<MemberDetail>().GetFirstOrDefaultAsync(a => a,
                 s => s.MemberDetailId == member.MemberDetailId);

            var memberAddress = await _unitOfWork.GetRepository<MemberAddress>().GetFirstOrDefaultAsync(a => a,
                 s => s.MemberId == member.MemberId);

            var memberSubscriptionPlan = await _unitOfWork.GetRepository<MemberSubscribedPlan>().GetPagedListAsync(a => a,
               s => s.MemberSubscription.MemberId == member.MemberId,
               pageIndex: BrokerConstants.PAGE_INDEX, pageSize: BrokerConstants.PAGE_SIZE);

            var memberSubscription = await _unitOfWork.GetRepository<MemberSubscription>().GetPagedListAsync(a => a,
               s => s.MemberId == member.MemberId,
               pageIndex: BrokerConstants.PAGE_INDEX, pageSize: BrokerConstants.PAGE_SIZE);

            var detailIds = dependents.Items.Select(d => d.MemberDetailId).ToList();

            var memberDemographicsBO = new MemberDemographicsBO();
            if (demographicsPermissionBO.SubscriberInfoPermission == MemberConstants.HasPermission)
                memberDemographicsBO = MemberMapper.Map(member, memberAddress, memberDetails, memberSubscription.Items);

            var dependentDetails = await _unitOfWork.GetRepository<MemberDetail>().GetPagedListAsync(a => a,
                predicate: md => detailIds.Contains(md.MemberDetailId),
                pageIndex: BrokerConstants.PAGE_INDEX, pageSize: BrokerConstants.PAGE_SIZE);

            if (dependentDetails != null && (demographicsPermissionBO.DependentInfoPermission == MemberConstants.HasPermission ||
                demographicsPermissionBO.ServiceInfoPermission == MemberConstants.HasPermission ||
                demographicsPermissionBO.DigitalIdCardPermission == MemberConstants.HasPermission))
            {
                var planIds = memberSubscriptionPlan.Items.Select(d => d.PlanId).ToList();
                var plans = await _unitOfWork.GetRepository<Plan>().GetPagedListAsync(a => a,
                        s => planIds.Contains(s.PlanId),
                   include: src => src.Include(e => e.Product),
                  pageIndex: BrokerConstants.PAGE_INDEX, pageSize: BrokerConstants.PAGE_SIZE);

                var paidThroughDate = memberSubscription.Items.Select(j => j.EndDate).FirstOrDefault();
                var levelOfCoverage = memberSubscriptionPlan.Items.Select(j => j.FamilyIndicator).FirstOrDefault();
                var planNames = new List<string>();
                var productNames = new List<string>();

                planNames = plans.Items.Select(p => p.Name).ToList();
                productNames = plans.Items.Select(p => p.Product.Name).ToList();
                var products = new List<ProductInformationBO>();
                foreach(var plan in plans.Items.OrderBy(p => p.Type))
                {
                    if (!string.IsNullOrEmpty(plan.Product.ProductLabel)
                        && plan.Product.ProductLabel.ToLower().Equals(MemberConstants.DentalVisionPlan))
                    {
                        var productDental = new ProductInformationBO
                        {
                            Name = plan.Product.Name,
                            ProductCode = plan.Product.ProductCode.Value,
                            ProductLabel = $"{MemberConstants.DentalPlan.Substring(0, 1).ToUpper()}{MemberConstants.DentalPlan.Substring(1)}"
                        };
                        products.Add(productDental);
                        var productVision = new ProductInformationBO
                        {
                            Name = plan.Product.Name,
                            ProductCode = plan.Product.ProductCode.Value,
                            ProductLabel = $"{MemberConstants.VisionPlan.Substring(0, 1).ToUpper()}{MemberConstants.VisionPlan.Substring(1)}"
                        };
                        products.Add(productVision);
                    }
                    else
                    {
                        var productInformation = new ProductInformationBO
                        {
                            Name = plan.Product.Name,
                            ProductCode = plan.Product.ProductCode.Value,
                            ProductLabel = (plan.Type == (int)PlanType.Primary)? 
                                            $"{MemberConstants.MedicalPlan.Substring(0, 1).ToUpper()}{MemberConstants.MedicalPlan.Substring(1)}"
                                            : plan.Product.ProductLabel
                        };
                        products.Add(productInformation);
                    }

                }
                if(products.Count(p => p.ProductLabel.ToLower() == MemberConstants.MedicalPlan)> 1)
                {
                    products.RemoveAt(1);
                }

                memberDemographicsBO.DependentInformation = new List<DependentInformationBO>();

                if (demographicsPermissionBO.ServiceInfoPermission == MemberConstants.HasPermission ||
                    demographicsPermissionBO.DigitalIdCardPermission == MemberConstants.HasPermission)
                {
                    memberDemographicsBO.ServiceInformation = new List<ServiceInformationBO>();
                    var subscriberCoverageInfo = MemberMapper.Map(member, memberDetails, planNames, productNames, products, levelOfCoverage, paidThroughDate);
                    memberDemographicsBO.ServiceInformation.Add(subscriberCoverageInfo);
                }

                foreach (var memDe in dependentDetails.Items)
                {
                    var status = await SetDependentStatus(memDe.MemberDetailId);

                    if (demographicsPermissionBO.DependentInfoPermission == MemberConstants.HasPermission)
                    {
                        var relationId = memberDependent.Where(a => a.MemberDetailId == memDe.MemberDetailId).Select(j => j.RelationshipId).FirstOrDefault();

                        var dependentInformation = new DependentInformationBO
                        {
                            DependentDetailId = memDe.MemberDetailId,
                            FirstName = memDe.FirstName,
                            LastName = memDe.LastName,
                            MemberName = memDe.FirstName + memDe.LastName,
                            DependentRelationship = ((Relationship)relationId).ToString(),
                            DateOfBirth = memDe.DateOfBirth,
                            DependentStatus = ((DependentStatus)status).ToString(),
                            // DependentStatus =
                            AvatarImageId = memDe.AvatarImageId,
                            PaidThroughDate = paidThroughDate
                        };
                        memberDemographicsBO.DependentInformation.Add(dependentInformation);
                    }

                    if (demographicsPermissionBO.ServiceInfoPermission == MemberConstants.HasPermission
                        || demographicsPermissionBO.DigitalIdCardPermission == MemberConstants.HasPermission)
                    {
                        var serviceInformation = new ServiceInformationBO
                        {
                            DependentDetailId = memDe.MemberDetailId,
                            FirstName = memDe.FirstName,
                            LastName = memDe.LastName,
                            MemberName = memDe.FirstName + memDe.LastName,
                            MemberType = MemberConstants.Dependent,
                            DigitalCardId = dependents.Items.Where(a => a.MemberDetailId == memDe.MemberDetailId).Select(a => a.DigitalIdcardId).FirstOrDefault() ?? 0,
                            PaidThroughDate = paidThroughDate,
                            PlanName = planNames,
                            Product = productNames,
                            ProductInformation = products,
                            LevelOfCoverage = ((FamilyIndicator)levelOfCoverage).ToString(),
                            DependentStatus = ((DependentStatus)status).ToString()
                        };
                        memberDemographicsBO.ServiceInformation.Add(serviceInformation);
                    }
                }

                memberDemographicsBO.DependentInformation = memberDemographicsBO.DependentInformation.OrderBy(a => a.DependentStatus).ToList();
            }

            return memberDemographicsBO;
        }

        /// <summary>
        /// Updates the member demographics.
        /// </summary>
        /// <param name="memberDemographics">The member demographics.</param>
        /// <param name="auditLogBO">The audit log bo.</param>
        /// <returns></returns>
        public async Task<int> UpdateMemberDemographics(MemberDemographicsBO memberDemographics, AuditLogBO auditLogBO)
        {
            var isUpdateCount = 0;
            var admin123Response = string.Empty;

            if (memberDemographics != null)
            {
                //Update Member PhoneNumber, Email & Address in Admin123
                admin123Response = await InvokeAdmin123Async(memberDemographics);

                if (admin123Response.Equals(HttpStatusCode.OK.ToString()))
                {
                    // Update Member PhoneNumber, Email & Address in MemberPortal Database
                    if (memberDemographics.MemberAddress != null)
                    {
                        var memberAddressRepo = _unitOfWork.GetRepository<MemberAddress>();
                        foreach (var addrToUpdate in memberDemographics.MemberAddress)
                        {
                            if (addrToUpdate.Id != 0)
                            {
                                var repoMemberAddress = memberAddressRepo.GetFirstOrDefault(a => a, b => b.MemberId == addrToUpdate.Id);

                                var existingMemberAddress = new MemberAddress();
                                existingMemberAddress = existingMemberAddress.Clone(repoMemberAddress);

                                if (repoMemberAddress != null)
                                {
                                    repoMemberAddress.AddressLine1 = addrToUpdate.AddressLine1;
                                    repoMemberAddress.AddressLine2 = addrToUpdate.AddressLine2;
                                    repoMemberAddress.City = addrToUpdate.City;
                                    repoMemberAddress.StateCode = addrToUpdate.StateCode;
                                    repoMemberAddress.ZipCode = addrToUpdate.ZipCode;
                                }
                                var isInvalidState = await ValidateState(addrToUpdate.StateCode);
                                if (!isInvalidState)
                                {
                                    _unitOfWork.GetRepository<MemberAddress>().Update(repoMemberAddress);
                                    isUpdateCount += _unitOfWork.SaveChanges();
                                }
                                else
                                    return (int)Utilities.Enumerations.ValidateState.StateInvalid;

                                //Log audit for update action on BrokerAddress
                                await AuditMapper.AuditLogging(auditLogBO, memberDemographics.MemberID, AuditAction.Update, repoMemberAddress, existingMemberAddress);
                            }
                        }
                    }
                    if (memberDemographics.MemberDetails != null && Convert.ToInt64(memberDemographics.MemberDetails.MemberId) != 0)
                    {
                        var memberDetailsRepo = _unitOfWork.GetRepository<MemberDetail>();
                        var memberContact = await memberDetailsRepo.GetFirstOrDefaultAsync(a => a, b => b.MemberDetailId == memberDemographics.MemberDetailId);

                        var existingMemberDetail = new MemberDetail();
                        existingMemberDetail = existingMemberDetail.Clone(memberContact);

                        memberContact.PhoneNumber = memberDemographics.MemberDetails.PhoneNumber;
                        memberContact.EmailId = memberDemographics.MemberDetails.Email;

                        _unitOfWork.GetRepository<MemberDetail>().Update(memberContact);
                        isUpdateCount += _unitOfWork.SaveChanges();

                        //Log audit for update action
                        await AuditMapper.AuditLogging(auditLogBO, memberDemographics.MemberDetailId, AuditAction.Update, memberContact, existingMemberDetail);
                    }
                }
            }

            if (isUpdateCount > 1)
                return (int)EditInformationStatus.Success;
            else
                return (int)EditInformationStatus.Fail;
        }

        /// <summary>
        /// Adds the dependent.
        /// </summary>
        /// <param name="dependentMember">The dependent member.</param>
        /// <param name="auditLogBO">The audit log bo.</param>
        /// <returns></returns>
        public async Task<(int, long)> AddDependent(DependentDetailsBO dependentMember, AuditLogBO auditLogBO)
        {
            var statusCode = DependentAddRemove.IsNotAdded;
            long dependentId = 0;

            var memberRepo = _unitOfWork.GetRepository<Member>();
            var memberInfo = await memberRepo.GetFirstOrDefaultAsync(a => a, b => b.MemberId == dependentMember.MemberId);
            var memberDetails = await _unitOfWork.GetRepository<MemberDetail>().GetFirstOrDefaultAsync(a => a,
                 s => s.MemberDetailId == memberInfo.MemberDetailId);

            var memberDependentRepo = _unitOfWork.GetRepository<MemberDependent>();
            var memberDependentDetailsRepo = _unitOfWork.GetRepository<MemberDetail>();
            var dependents = await memberDependentRepo.GetPagedListAsync(a => a, predicate: b => b.MemberId == dependentMember.MemberId);
            var dependentIds = dependents.Items.Select(i => i.MemberDetailId).ToList();

            var dependentDetails = await memberDependentDetailsRepo.GetPagedListAsync(a => a, predicate: b => dependentIds.Contains(b.MemberDetailId));
            var checkdependent = dependentDetails.Items.Where(d => (d.FirstName == dependentMember.FirstName) &&
                            (d.LastName == dependentMember.LastName) &&
                        (Convert.ToString(d.DateOfBirth.Date.ToString("yyyy-MM-dd")) == dependentMember.DOB) &&
                        (((Relationship)dependents.Items.FirstOrDefault(dep => dep.MemberDetailId == d.MemberDetailId).RelationshipId).ToString() == dependentMember.Relationship)).ToList();

            if (checkdependent.Any())
            {
                statusCode = DependentAddRemove.IsExisting;
                return ((int)statusCode, 0);
            }

            var memberSubscriptionPlan = await _unitOfWork.GetRepository<MemberSubscribedPlan>().GetPagedListAsync(a => a,
              s => s.MemberSubscription.MemberId == dependentMember.MemberId,
              pageIndex: BrokerConstants.PAGE_INDEX, pageSize: BrokerConstants.PAGE_SIZE);
            await _unitOfWork.GetRepository<MemberSubscription>().GetPagedListAsync(a => a,
                s => s.MemberId == dependentMember.MemberId,
                pageIndex: BrokerConstants.PAGE_INDEX, pageSize: BrokerConstants.PAGE_SIZE);

            var existingFamilyIndicator = memberSubscriptionPlan.Items.Select(a => a.FamilyIndicator).FirstOrDefault();

            if (dependentMember != null)
            {
                var memberDependentId = 0;
                //Adding Details of Dependent in table

                var member = await _unitOfWork.GetRepository<Member>()
                      .GetFirstOrDefaultAsync(a => a, obj => obj.MemberId == dependentMember.MemberId);

                var details = await _unitOfWork.GetRepository<MemberDetail>()
                        .GetFirstOrDefaultAsync(a => a, obj => obj.MemberDetailId == member.MemberDetailId);

                var memberDetail = MemberMapper.Map(dependentMember, details);
                await _unitOfWork.GetRepository<MemberDetail>().InsertAsync(memberDetail);
                var dependentMemberDetailId = await _unitOfWork.SaveChangesAsync();

                if (dependentMemberDetailId > 0)
                {
                    var memberDependent = MemberMapper.Map(dependentMember, memberDetail.MemberDetailId);

                    dependentId = memberDetail.MemberDetailId;
                    var updatedFamilyIndicator = await UpdateFamilyIndicator(dependentMember.RelationshipId, existingFamilyIndicator, dependentMember.MemberId, "Add");

                    if (updatedFamilyIndicator > 0)
                    {
                        memberSubscriptionPlan.Items.All(msp => { msp.FamilyIndicator = updatedFamilyIndicator; return true; });
                        _unitOfWork.GetRepository<MemberSubscribedPlan>().Update(memberSubscriptionPlan.Items);
                    }
                    await _unitOfWork.GetRepository<MemberDependent>().InsertAsync(memberDependent);
                    memberDependentId = await _unitOfWork.SaveChangesAsync();
                }

                if (memberDependentId > 0)
                {
                    var memberAddress = await _unitOfWork.GetRepository<MemberAddress>()
                        .GetFirstOrDefaultAsync(a => a, obj => obj.MemberId == dependentMember.MemberId);

                    var memberDependentAddress = new MemberDependentAddress();
                    {
                        memberDependentAddress.MemberId = dependentMember.MemberId;
                        memberDependentAddress.MemberDetailId = memberDetail.MemberDetailId;
                        memberDependentAddress.AddressTypeId = memberAddress.AddressTypeId;
                        memberDependentAddress.AddressLine1 = memberAddress.AddressLine1;
                        memberDependentAddress.AddressLine2 = memberAddress.AddressLine2;
                        memberDependentAddress.City = memberAddress.City;
                        memberDependentAddress.ZipCode = memberAddress.ZipCode.Trim();
                        memberDependentAddress.StateCode = memberAddress.StateCode;
                        memberDependentAddress.CreatedOn = DateTime.Now;
                        memberDependentAddress.CreatedBy = memberAddress.MemberId;

                        //var isInvalidState = await ValidateState(memberAddress.StateCode);
                        //if (!isInvalidState)
                        //{
                        await _unitOfWork.GetRepository<MemberDependentAddress>().InsertAsync(memberDependentAddress);
                        var rowCount = await _unitOfWork.SaveChangesAsync();
                        if (rowCount > 0)
                            statusCode = DependentAddRemove.IsAdded;
                        //}
                        //else
                        //    return ((int)Utilities.Enumerations.ValidateState.StateInvalid, 0);
                    }
                }

                if (dependentMember.MemberDependentQuestionAnswersBO != null)
                {
                    await SaveDependentMemberQuestionAnswerAsync(dependentMember.MemberDependentQuestionAnswersBO,
                        memberDetail.MemberDetailId, member.MemberId);
                }
            }

            if (statusCode == DependentAddRemove.IsAdded)
            {
                //Adding Dependent Information to Member Dependent Status History
                {
                    var statusHistory = new List<MemberDependentStatusHistory>();

                    statusHistory.AddRange(memberSubscriptionPlan.Items.Select(q => new MemberDependentStatusHistory

                    {
                        MemberId = dependentMember.MemberId,
                        MemberDetailId = dependentId,
                        MemberSubscribedPlanId = q.MemberSubscribedPlanId,
                        CreatedBy = dependentMember.MemberId,
                        CreatedOn = DateTime.UtcNow
                    }));

                    await _unitOfWork.GetRepository<MemberDependentStatusHistory>().InsertAsync(statusHistory);
                    await _unitOfWork.SaveChangesAsync();
                }
                var mailTo = _appSettings.Value.PlanChangeRequestEmail;
                var notificaton = await _unitOfWork.GetRepository<Notification>().GetFirstOrDefaultAsync(predicate: n => n.Type == (int)EmailType.MemberDependentActivation);
                var questionIds = dependentMember.MemberDependentQuestionAnswersBO
                    .Select(q => q.DependentQuestionnaireId).ToList();
                var memberDependentQuestions = await _unitOfWork.GetRepository<MemberQuestionnaire>().GetPagedListAsync(
                    a => new { a.MemberQuestionnaireId, a.MemberQuestionnaire1 },
                    predicate: question => questionIds.Contains(question.MemberQuestionnaireId),
                    pageIndex: BrokerConstants.PAGE_INDEX, pageSize: BrokerConstants.PAGE_SIZE);
                var questionsContext = string.Empty;
                var answerValue = string.Empty;
                var i = 1;
                for (int item = 0; item < memberDependentQuestions.Items.Count; item++)
                {
                    if (answerValue.ToLower().Equals("no"))
                    {
                        answerValue = string.Empty;
                        continue;
                    }

                    var answer = dependentMember.MemberDependentQuestionAnswersBO.Where(q =>
                        q.DependentQuestionnaireId == memberDependentQuestions.Items[item]
                            .MemberQuestionnaireId).Select(ans => ans.DependentAnswer).ToArray();
                    answerValue = answer.Length > 0 && !string.IsNullOrEmpty(answer[0]) ? answer[0] : "No Inputs";

                    questionsContext = questionsContext + "<br/>" + "Q" + i + ". " +
                                       memberDependentQuestions.Items[item].MemberQuestionnaire1 + "<br/>Ans: " +
                                       answerValue.Substring(0, 1).ToUpper() + answerValue.Substring(1);
                    i++;
                }

                if (notificaton != null && dependentMember != null)
                {
                    //var isTobaccoUser = dependentMember.isTobaccoUser ? "Yes" : "No";
                    var subject = notificaton.Subject.Replace("{externalId}", memberInfo.ExternalId);
                    var body = notificaton.Content.Replace("{firstname}", memberDetails.FirstName)
                        .Replace("{lastname}", memberDetails.LastName)
                        .Replace("{externalId}", memberInfo.ExternalId)
                        .Replace("{dependentfirstname}", dependentMember.FirstName)
                        .Replace("{dependentlastname}", dependentMember.LastName)
                        .Replace("{relationship}", Convert.ToString((Relationship)dependentMember.RelationshipId))
                        .Replace("{gender}",
                            char.ToUpper(dependentMember.Gender[0]) + dependentMember.Gender.Substring(1))
                        .Replace("{dateofbirth}", dependentMember.DOB)
                        //.Replace("{istobaccouser}", isTobaccoUser)
                        .Replace("{questionnaire}", questionsContext);

                    var email = MemberMapper.Map(mailTo, body, subject, _appSettings, notificaton.From);
                    await _emailService.SendEmailAsync(email);
                }
            }
            return ((int)statusCode, dependentId);
        }

        /// <summary>
        /// Deletes the dependent.
        /// </summary>
        /// <param name="dependentDetailId">The dependent detail identifier.</param>
        /// <param name="auditLogBO">The audit log bo.</param>
        /// <returns></returns>
        public async Task<int> DeleteDependent(long dependentDetailId, AuditLogBO auditLogBO)
        {
            var memberRepo = _unitOfWork.GetRepository<Member>();
            var statusHistoryRepo = _unitOfWork.GetRepository<MemberDependentStatusHistory>();
            var memberDependents = await _unitOfWork.GetRepository<MemberDependent>().GetFirstOrDefaultAsync(a => a, obj => obj.MemberDetailId == dependentDetailId);
            var memberDependentDetails = await _unitOfWork.GetRepository<MemberDetail>().GetFirstOrDefaultAsync(a => a, obj => obj.MemberDetailId == dependentDetailId);
            var memberId = memberDependents.MemberId;
            var member = await memberRepo.GetFirstOrDefaultAsync(a => a, b => b.MemberId == memberId);
            var memberDetails = await _unitOfWork.GetRepository<MemberDetail>().GetFirstOrDefaultAsync(a => a,
                 s => s.MemberDetailId == member.MemberDetailId);
            var memberDependentStatusHistory = await statusHistoryRepo.GetPagedListAsync(a => a, predicate: b => b.MemberDetailId == dependentDetailId,
                pageIndex: BrokerConstants.PAGE_INDEX, pageSize: BrokerConstants.PAGE_SIZE);
            var mailTo = _appSettings.Value.PlanChangeRequestEmail;

            var notificaton = await _unitOfWork.GetRepository<Notification>()
                .GetFirstOrDefaultAsync(predicate: n => n.Type == (int)EmailType.MemberDependentInactivation);
            if (notificaton != null)
            {
                var subject = notificaton.Subject.Replace("{externalId}", member.ExternalId);
                var body = notificaton.Content.Replace("{firstname}", memberDetails.FirstName)
                    .Replace("{lastname}", memberDetails.LastName).Replace("{externalId}", member.ExternalId)
                    .Replace("{dependentexternalId}", memberDependents.ExternalId)
                    .Replace("{dependentfirstname}", memberDependentDetails.FirstName)
                    .Replace("{dependentlastname}", memberDependentDetails.LastName)
                    .Replace("{relationship}", Convert.ToString((Relationship)memberDependents.RelationshipId))
                    .Replace("{gender}", char.ToUpper(memberDependentDetails.Gender[0]) + memberDependentDetails.Gender.Substring(1))
                    .Replace("{dateofbirth}", Convert.ToString(memberDependentDetails.DateOfBirth).Substring(0, Convert.ToString(memberDependentDetails.DateOfBirth).IndexOf(" ")));

                var email = MemberMapper.Map(mailTo, body, subject, _appSettings, notificaton.From);
                await _emailService.SendEmailAsync(email);
            }

            memberDependents.DeactiveDate = DateTime.Now;
            _unitOfWork.GetRepository<MemberDependent>().Update(memberDependents);

            await _unitOfWork.SaveChangesAsync();

            return (int)DependentAddRemove.IsDeleted;
        }

        /// <summary>
        /// Updates the family indicator.
        /// </summary>
        /// <param name="relationshipId">The relationship identifier.</param>
        /// <param name="existingFamilyIndicator">The existing family indicator.</param>
        /// <param name="memberId">The member identifier.</param>
        /// <param name="operation">The operation.</param>
        /// <returns></returns>
        public async Task<int> UpdateFamilyIndicator(int relationshipId, int existingFamilyIndicator, long memberId, string operation)
        {
            var newIndicator = 0;
            var countChild = 0;
            var memberDependent = await _unitOfWork.GetRepository<MemberDependent>().GetPagedListAsync(a => a,
                      s => s.MemberId == memberId && (s.RelationshipId == 6 || s.RelationshipId == 7),
                      pageIndex: BrokerConstants.PAGE_INDEX, pageSize: BrokerConstants.PAGE_SIZE);

            countChild = memberDependent.Items.Count;

            switch (operation)
            {
                case "Add":
                    switch (existingFamilyIndicator)
                    {
                        case 1:
                            if (relationshipId == 2)
                                newIndicator = 2;
                            else if (relationshipId == 6 || relationshipId == 7)
                                newIndicator = 4;
                            break;

                        case 2:
                            newIndicator = 3;
                            break;

                        case 3:
                            newIndicator = 3;
                            break;

                        case 4:
                            if (relationshipId == 2)
                                newIndicator = 3;
                            else if (relationshipId == 6 || relationshipId == 7)
                                newIndicator = 4;
                            break;

                        default:
                            break;
                    }
                    break;

                case "Delete":
                    switch (existingFamilyIndicator)
                    {
                        case 2:
                            newIndicator = 1;
                            break;

                        case 3:
                            if (relationshipId == 2)
                                newIndicator = 4;
                            else if (relationshipId == 6 || relationshipId == 7)
                                newIndicator = countChild > 1 ? 3 : 2;
                            break;

                        case 4:
                            //check for multiple child
                            newIndicator = countChild > 1 ? 4 : 1;
                            break;

                        default:
                            break;
                    }
                    break;
            }
            return newIndicator;
        }

        /// <summary>
        /// Sets the dependent status.
        /// </summary>
        /// <param name="dependentMemberDetailId">The dependent member detail identifier.</param>
        /// <returns></returns>
        public async Task<int> SetDependentStatus(long dependentMemberDetailId)
        {
            var memberDependentStatusHistoryRepo = _unitOfWork.GetRepository<MemberDependentStatusHistory>();

            var dependentsHistory = await memberDependentStatusHistoryRepo
                .GetPagedListAsync(a => a, predicate: b => b.MemberDetailId == dependentMemberDetailId);

            var dependentStatus = DependentStatus.NoStatus;

            var memberDependentRepo = _unitOfWork.GetRepository<MemberDependent>();
            var dependent = await memberDependentRepo.GetFirstOrDefaultAsync(a => a,
                predicate: b => b.MemberDetailId == dependentMemberDetailId);

            if (dependentsHistory.Items.Count > 0)
            {
                dependentStatus = (dependentsHistory.Items.Any(y => y.InActiveDate != null)) ?
                    (dependentsHistory.Items.Any(x => x.InActiveDate > DateTime.UtcNow) && dependent.DeactiveDate != null && (dependentsHistory.Items.Any(x => x.ActiveDate <= DateTime.UtcNow))) ?
                      DependentStatus.PendingInactivation : dependentsHistory.Items.Any(x => x.ActiveDate != null)
                      && dependent.DeactiveDate != null && (dependentsHistory.Items.Where(x => x.InActiveDate != null).Select(x => x.MemberId).Count() ==
                        dependentsHistory.Items.Select(x => x.MemberId).Count()) ? DependentStatus.Inactive : DependentStatus.Active
                    :
                     (dependentsHistory.Items.Any(y => y.ActiveDate != null) &&
                     (dependentsHistory.Items.Any(x => x.ActiveDate <= DateTime.UtcNow))) ?
                       (dependentsHistory.Items.Any(x => x.ActiveDate <= DateTime.UtcNow)
                       && dependent.DeactiveDate == null) ? DependentStatus.Active :
                       DependentStatus.PendingInactivation : DependentStatus.PendingActivation;
            }
            else
            {
                dependentStatus = dependent.DeactiveDate != null
                    ? DependentStatus.PendingInactivation
                    : (String.IsNullOrWhiteSpace(dependent.ExternalId) && dependent.DeactiveDate == null ?
                        DependentStatus.PendingActivation : DependentStatus.Active);
            }

            #region

            //////Do not remove the commented code. Will be removed after confirmation of HRP process
            //var memberDependentRepo = _unitOfWork.GetRepository<MemberDependent>();
            //var dependents = await memberDependentRepo.GetFirstOrDefaultAsync(a => a, predicate: b => b.MemberDetailId == dependentMemberDetailId);
            //var memberSubscription = await _unitOfWork.GetRepository<MemberSubscription>().GetFirstOrDefaultAsync(a => a,
            //  s => s.MemberId == memberId);
            //var memberPaymentDetails = await _unitOfWork.GetRepository<MemberPaymentDetail>().GetPagedListAsync(a => a,
            //    s => s.MemberId == memberId,
            //  pageIndex: BrokerConstants.PAGE_INDEX, pageSize: BrokerConstants.PAGE_SIZE);
            //var lastTransactionDetails = memberPaymentDetails.Items.OrderByDescending(a => a.PaidDate).Take(1).FirstOrDefault();

            //var dependentStatus = DependentStatus.NoStatus;

            //var currentDate = DateTime.Now;
            //var firstPaymentDate = memberSubscription.FirstPaymentDate;

            //var lastPaymentDate = lastTransactionDetails != null ? lastTransactionDetails.PaidDate : firstPaymentDate;
            //var dependentexternalId = dependents.ExternalId;

            //if (String.IsNullOrWhiteSpace(dependentexternalId) && dependents.DeactiveDate == null)
            //    dependentStatus = DependentStatus.PendingActivation;
            //else
            //{
            //    if (dependents.DeactiveDate == null)
            //        dependentStatus = dependents.CreatedOn > lastPaymentDate ? DependentStatus.PendingActivation : DependentStatus.Active;
            //    else
            //        dependentStatus = dependents.DeactiveDate < currentDate ? DependentStatus.Inactive : DependentStatus.PendingInactivation;
            //}

            #endregion

            return (int)dependentStatus;
        }

        /// <summary>
        /// Validates the state.
        /// </summary>
        /// <param name="stateCode">The state code.</param>
        /// <returns></returns>
        public async Task<bool> ValidateState(string stateCode)
        {
            var isInvalidState = false;
            var notAllowedStates = await _unitOfWork.GetRepository<MasterData>().GetFirstOrDefaultAsync(a => a,
                obj => obj.Key == MemberConstants.EnrollmentNotAllowedStates);
            var statesList = Convert.ToString(notAllowedStates.Value).Split(',').ToList();
            foreach (var item in statesList)
            {
                if (item.Trim() == stateCode.Trim())
                    isInvalidState = true;
            }

            return isInvalidState;
        }

        /// <summary>
        /// Member Dependent Questionnaires
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public async Task<List<MemberQuestionnaireBO>> GetMemberQuestionnairesAsync(int type)
        {
            var dependentQuestionnaires = await _unitOfWork.GetRepository<MemberQuestionnaire>().GetPagedListAsync(a => a,
                obj => obj.MemberQuestionnaireType == type,
                pageIndex: BrokerConstants.PAGE_INDEX, pageSize: BrokerConstants.PAGE_SIZE);

            return MemberMapper.Map(dependentQuestionnaires.Items);
        }

        /// <summary>
        /// Save Dependent Member Question Answer
        /// </summary>
        /// <param name="memberDependentQuestionAnswers"></param>
        /// <param name="dependentDetailId"></param>
        /// <param name="memberId"></param>
        /// <returns></returns>
        private async Task SaveDependentMemberQuestionAnswerAsync(List<MemberDependentQuestionAnswersBO> memberDependentQuestionAnswers, long dependentDetailId, long memberId)
        {
            var dependentMemberQuestionAnswersAdd = new List<MemberDependentQuestionAnswer>();
            dependentMemberQuestionAnswersAdd.AddRange(memberDependentQuestionAnswers.Where(
                i => !string.IsNullOrEmpty(i.DependentAnswer)).Select(q =>
                new MemberDependentQuestionAnswer
                {
                    MemberId = memberId,
                    MemberDetailId = dependentDetailId,
                    DepenedentQuestionnaireId = q.DependentQuestionnaireId,
                    DependentAnswer = q.DependentAnswer
                }));

            await _unitOfWork.GetRepository<MemberDependentQuestionAnswer>().InsertAsync(dependentMemberQuestionAnswersAdd);

            await _unitOfWork.SaveChangesAsync();
        }

        private async Task<string> InvokeAdmin123Async(MemberDemographicsBO memberDemographics)
        {
            var admin123 = new Admin123Mapper(_unitOfWork);
            try
            {
                //Bind data to Admin123BO

                var admin123BO = new Admin123BO
                {
                    CORPID = _appSettings.Value.CorpId,
                    AGENT = _appSettings.Value.AgentId, //UAT or demo replace with this code//memberSummary.BrokerId,
                    //UNIQUEID = $"{memberDemographics.ExternalMemberId}-{AppSettings.Environment}",
                    LASTNAME = memberDemographics.LastName,
                    ADDRESS1 = memberDemographics.Address1,
                    ADDRESS2 = string.IsNullOrEmpty(memberDemographics.Address2.Trim()) ? " " : memberDemographics.Address2,
                    CITY = memberDemographics.City,
                    STATE = memberDemographics.StateProvince,
                    ZIPCODE = memberDemographics.ZipCode,
                    EMAIL = memberDemographics.Email,
                    PHONE1 = memberDemographics.PhoneNumber
                };

                var memberId = memberDemographics.ExternalMemberId;
                var memberObject = JsonConvert.SerializeObject(admin123BO);
                var uri = new Uri(_appSettings.Value.Admin123Url);
                var stringContent = new StringContent(memberObject, UnicodeEncoding.UTF8, "application/json");
                var t = Task.Run(() => admin123.PostURIAsync(uri, stringContent, _appSettings.Value.Admin123User, _appSettings.Value.Admin123Password, memberObject, memberId));
                t.Wait();
                return t.Result;
            }
            catch (Exception ex)
            {
                await admin123.InsertAdmin123LogAsync(ex.StackTrace, ex.Message.ToString());
                throw;
            }
        }
    }
}