using System.Xml.Serialization;

namespace Aliera.BusinessObjects.HRP
{
    public class SubscriptionBO
    {
        [XmlElement("autoWithdrawalEnabled")]
        public bool AutowithdrawalEnabled { get; set; }

        [XmlElement("subscriptionUDTList")]
        public SubscriptionudtlistBO SubscriptionudtList { get; set; }

        [XmlElement("accountMatchData")]
        public AccountmatchdataBO AccountMatchData { get; set; }
    }

    public class SubscriptionudtlistBO
    {
        [XmlElement("listMode")]
        public string ListMode { get; set; }

        [XmlElement("subscriptionUDT")]
        public SubscriptionudtBO Subscriptionudt { get; set; }
    }

    public class SubscriptionudtBO
    {
        [XmlElement("udtListValueSet")]
        public UdtlistvaluesetBO UdtLisValueSet { get; set; }

        [XmlElement("userDefinedTermReference")]
        public UserdefinedtermreferenceBO UserDefinedTermReference { get; set; }
    }

    public class UdtlistvaluesetBO
    {
        [XmlElement("attributeRoleName")]
        public string AttributeRoleName { get; set; }

        [XmlElement("attrValueAsString")]
        public string AttrvalueAsAtring { get; set; }
    }

    public class UserdefinedtermreferenceBO
    {
        [XmlElement("ID")]
        public string Id { get; set; }
    }

    public class AccountmatchdataBO
    {
        [XmlElement("accountHccIdentifier")]
        public AccounthccidentifierBO AccounthccIdentifier { get; set; }
    }

    public class AccounthccidentifierBO
    {
        [XmlElement("accountHccIdentificationNumber")]
        public string AccounthccIdentificationNumber { get; set; }
    }

    public class MembermatchdataBO
    {
        [XmlElement("definitionName")]
        public string DefinitionName { get; set; }

        [XmlElement("member")]
        public MemberHRPBO Member { get; set; }
    }

    public class MemberHRPBO
    {
        [XmlElement("firstName")]
        public string FirstName { get; set; }
    }

    public class IndividualBO
    {
        [XmlElement("genderCode")]
        public string GenderCode { get; set; }

        [XmlElement("maritalStatusCode")]
        public string MaritalStatusCode { get; set; }

        [XmlElement("birthDate")]
        public string Birthdate { get; set; }

        [XmlElement("primaryName")]
        public PrimarynameBO PrimaryName { get; set; }

        [XmlElement("languages")]
        public LanguagesBO Languages { get; set; }
    }

    public class PrimarynameBO
    {
        [XmlElement("lastName")]
        public string LastName { get; set; }

        [XmlElement("firstName")]
        public string FirstName { get; set; }
    }
}
