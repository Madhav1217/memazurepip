namespace Aliera.BusinessObjects.Broker
{
    public class BrokerContactBO
    {
        public long BrokerContactId { get; set; }
        public long BrokerId { get; set; }
        public string PhoneNumber { get; set; }
        public string Ext { get; set; }
        public string AlternatePhoneNumber { get; set; }        
        public string FaxNumber { get; set; }
        public string EmailId { get; set; }
        public string AlternateEmailId { get; set; }
        public string Website { get; set; }
    }
}
