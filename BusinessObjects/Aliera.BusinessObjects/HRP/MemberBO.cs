using System.Collections.Generic;
using System.Xml.Serialization;

namespace Aliera.BusinessObjects.HRP
{
    public class MemberBO
    {
        [XmlElement("maintenanceTypeCode")]
        public string MaintenanceTypeCode { get; set; }

        [XmlElement("maintenanceOverride")]
        public bool MaintenanceOverride { get; set; }

        [XmlElement("memberIsSubscriber")]
        public bool MemberisSubscriber { get; set; }

        [XmlElement("smokingStatus")]
        public string SmokingStatus { get; set; }

        [XmlElement("isHandicapped")]
        public bool IsHandicapped { get; set; }

        [XmlElement("isMemberInHospice")]
        public bool IsMemberInHospice { get; set; }

        [XmlElement("memberMatchData")]
        public MembermatchdataBO MemberMatchData { get; set; }

        [XmlElement("individual")]
        public IndividualBO Individual { get; set; }

        [XmlElement("physicalAddress")]
        public PhysicaladdressBO PhysicalAddress { get; set; }

        [XmlElement("correspondenceAddress")]
        public CorrespondenceaddressBO CorrespondenceAddress { get; set; }

        [XmlElement("otherIdNumberList")]
        public OtheridnumberlistBO OtherIdNumberList { get; set; }

        [XmlElement("relationshipToSubscriberDefinitionReference")]
        public RelationshiptosubscriberdefinitionreferenceBO RelationshipToSubscriberDefinitionReference { get; set; }

        [XmlElement("planSelection")]
        public List<PlanselectionBO> PlanSelection { get; set; }
    }
}
