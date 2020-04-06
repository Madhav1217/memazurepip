using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Aliera.Utilities.Helpers
{
    [XmlRoot("member")]
    public class MemberXML
    {
        [XmlElement("transactionId")]
        public string TransactionId { get; set; }
        [XmlElement("asOfDate")]
        public DateTime AsOfDate { get; set; }
        [XmlElement("ActiveDate")]
        public DateTime ActiveDate { get; set; }
        [XmlElement("subscription")]
        public Subscription subscription = new Subscription();
        [XmlElement("member")]
        public MemberInfo member = new MemberInfo();



    }

    public class Subscription 
    {
        
    }

    public class MemberInfo
    {
        [XmlElement("maintenanceTypeCode")]
        public string MaintenanceTypeCode { get; set; }
        [XmlElement("maintenanceOverride")]
        public bool MaintenanceOverride { get; set; }
        [XmlElement("memberIsSubscriber")]
        public bool MemberIsSubscriber { get; set; }
        [XmlElement("smokingStatus")]
        public string SmokingStatus { get; set; }
        [XmlElement("isHandicapped")]
        public bool IsHandicapped { get; set; }
        [XmlElement("isMemberInHospice")]
        public bool IsMemberInHospice { get; set; }

        [XmlElement("memberMatchData")]
        public MemberMatchData memberMatchData = new MemberMatchData();

        [XmlElement("individual")]
        public Individual individual = new Individual();

        [XmlElement("physicalAddress")]
        public PhysicalAddress physicalAddress = new PhysicalAddress();
    }

    public class MemberMatchData
    {
        [XmlElement("definitionName")]
        public string DefinitionName { get; set; }
    }

    public class Individual
    {
        [XmlElement("genderCode")]
        public string GenderCode { get; set; }
        [XmlElement("maritalStatusCode")]
        public string MaritalStatusCode { get; set; }
        [XmlElement("birthDate")]
        public DateTime BirthDate { get; set; }

        [XmlElement("primaryName")]
        public PrimaryName primaryName = new PrimaryName();

        [XmlElement("languages")]
        public Languages languages = new Languages();

    }

    public class PrimaryName
    {
        [XmlElement("firstName")]
        public string FirstName { get; set; }
        [XmlElement("lastName")]
        public string LastName { get; set; }
    }

    public class Languages
    {
        [XmlElement("listMode")]
        public string ListMode { get; set; }

        [XmlElement("language")]
        public Language language = new Language();
    }

    public class Language
    {
        [XmlElement("primaryLanguage")]
        public bool PrimaryLanguage { get; set; }

        [XmlElement("languageDomainCode")]
        public LanguageDomainCode languageDomainCode = new LanguageDomainCode();
    }

    public class LanguageDomainCode
    {
        [XmlElement("codeSetName")]
        public string CodeSetName { get; set; }

        [XmlElement("codeEntry")]
        public string CodeEntry { get; set; }
    }

    public class PhysicalAddress
    {
        [XmlElement("memberPhysicalAddress")]
        public MemberPhysicalAddress memberPhysicalAddress = new MemberPhysicalAddress();

        [XmlElement("correspondenceAddress")]
        public CorrespondenceAddress correspondenceAddress = new CorrespondenceAddress();

        [XmlElement("otherIdNumberList")]
        public OtherIdNumberList otherIdNumberList = new OtherIdNumberList();

        [XmlElement("relationshipToSubscriberDefinitionReference")]
        public RelationshipToSubscriberDefinitionReference relationshipToSubscriberDefinitionReference = new RelationshipToSubscriberDefinitionReference();

        [XmlElement("planSelection")]
        public PlanSelection planSelection = new PlanSelection();


        

    }

    public class MemberPhysicalAddress
    {
        [XmlElement("emailAddress")]
        public string EmailAddress { get; set; }

        [XmlElement("addressInfo")]
        public AddressInfo addressInfo = new AddressInfo();

        [XmlElement("addressTypeCode")]
        public AddressTypeCode addressTypeCode = new AddressTypeCode();

    }

    public class AddressInfo
    {
        
        [XmlElement("postalAddress")]
        public PostalAddress postalAddress = new PostalAddress();

        [XmlElement("addressPhoneList")]
        public AddressPhoneList addressPhoneList = new AddressPhoneList();

    }

    public class PostalAddress
    {
        [XmlElement("address")]
        public string Address { get; set; }
        [XmlElement("stateCode")]
        public string StateCode { get; set; }
        [XmlElement("zipCode")]
        public string ZipCode { get; set; }
        [XmlElement("cityName")]
        public string CityName { get; set; }
        [XmlElement("ignoreAddressCheck")]
        public bool IgnoreAddressCheck { get; set; }

    }

    public class AddressPhoneList
    {

        [XmlElement("telephoneNumber")]
        public TelephoneNumber telephoneNumber = new TelephoneNumber();
  
    }

    public class TelephoneNumber
    {

        [XmlElement("phoneCountryCode")]
        public string PhoneCountryCode { get; set; }
        [XmlElement("phoneAreaCode")]
        public string PhoneAreaCode { get; set; }
        [XmlElement("phoneNumber")]
        public string PhoneNumber { get; set; }
    }

    public class AddressTypeCode
    {

        [XmlElement("codeSetName")]
        public string CodeSetName { get; set; }

        [XmlElement("codeEntry")]
        public int CodeEntry { get; set; }
    }

    public class CorrespondenceAddress
    {

        [XmlElement("postalAddress")]
        public PostalAddress postalAddress = new PostalAddress();

    }

    public class OtherIdNumberList
    {

        [XmlElement("listMode")]
        public string ListMode { get; set; }


        [XmlElement("identificationNumber")]
        public List<IdentificationNumber> identificationNumber = new List<IdentificationNumber>();

    }

    public class IdentificationNumber
    {

        [XmlElement("identificationNumber")]
        public string IdNumber { get; set; }

        [XmlElement("identificationTypeCode")]
        public IdentificationTypeCode identificationTypeCode = new IdentificationTypeCode();

    }

    public class IdentificationTypeCode
    {

        [XmlElement("codeSetName")]
        public string CodeSetName { get; set; }

        [XmlElement("codeEntry")]
        public string CodeEntry { get; set; }

    }

    public class RelationshipToSubscriberDefinitionReference
    {

        [XmlElement("reference")]
        public Reference reference = new Reference();

        [XmlElement("relationshipName")]
        public string RelationshipName { get; set; }
    }

    public class Reference
    {
  
        [XmlElement("ID")]
        public int id { get; set; }
    }

    public class PlanSelection
    {

        [XmlElement("startDate")]
        public DateTime StartDate { get; set; }

        [XmlElement("benefitPlanMatchData")]
        public BenefitPlanMatchData benefitPlanMatchData = new BenefitPlanMatchData();
    }

    public class BenefitPlanMatchData
    {

        [XmlElement("benefitPlanHccId")]
        public string BenefitPlanHccId { get; set; }
    }

}

