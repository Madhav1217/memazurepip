using System.ComponentModel.DataAnnotations;

namespace Aliera.BusinessObjects.Broker
{
    public class MemberAddressBO
    {
        [Key]
        public long Id { get; set; }
        public int AddressType { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string StateName { get; set; }
        public string StateCode { get; set; }
        public string ZipCode { get; set; }
    }
}
