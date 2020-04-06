using System;

namespace Aliera.BusinessObjects.Broker
{
    public class MemberValidationBO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StateCode { get; set; }
        public string Zipcode { get; set; }
        public string ExternalId { get; set; }
        public string Admin123Id { get; set; }
        public DateTime DOB { get; set; }
    }
}
