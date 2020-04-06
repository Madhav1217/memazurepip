using System.Collections.Generic;
using System.Xml.Serialization;

namespace Aliera.BusinessObjects.HRP
{
    [XmlRoot("EnrollmentType")]
    public class HRPDataBO
    {
        [XmlElement("transactionId")]
        public string Transactionid { get; set; }

        [XmlElement("asOfDate")]
        public string AsOfDate { get; set; }

        [XmlElement("activate")]
        public bool Activate { get; set; }

        [XmlElement("cascadeTerms")]
        public bool Cascadeterms { get; set; }

        [XmlElement("cascadeCancels")]
        public bool Cascadecancels { get; set; }

        [XmlElement("subscription")]
        public SubscriptionBO Subscription { get; set; }

        [XmlElement("member")]
        public List<MemberBO> Member { get;set; }
    }
}
