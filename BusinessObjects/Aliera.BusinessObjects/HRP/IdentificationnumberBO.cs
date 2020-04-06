using System.Xml.Serialization;

namespace Aliera.BusinessObjects.HRP
{
    public class IdentificationnumberBO
    {
        [XmlElement("identificationNumber")]
        public string IdentificationNumber { get; set; }

        [XmlElement("identificationTypeCode")]
        public IdentificationtypecodeBO IdentificationTypeCode { get; set; }
    }

    public class IdentificationtypecodeBO
    {
        [XmlElement("codeSetName")]
        public string CodeSetName { get; set; }

        [XmlElement("codeEntry")]
        public string CodeEntry { get; set; }
    }

    public class RelationshiptosubscriberdefinitionreferenceBO
    {
        [XmlElement("reference")]
        public ReferenceBO Reference { get; set; }

        [XmlElement("relationshipName")]
        public string Relationshipname { get; set; }
    }

    public class ReferenceBO
    {
        [XmlElement("ID")]
        public string Id { get; set; }
    }

    public class PlanselectionBO
    {
        [XmlElement("startDate")]
        public string StartDate { get; set; }

        [XmlElement("benefitPlanMatchData")]
        public BenefitplanmatchdataBO BenefitPlanMatchData { get; set; }
    }

    public class BenefitplanmatchdataBO
    {
        [XmlElement("benefitPlanHccId")]
        public long? BenefitPlanhccId { get; set; }
    }
}
