using System;
using System.Collections.Generic;
using System.Text;

namespace Aliera.BusinessObjects.Broker
{
    public class GroupBO
    {
        public long GroupId { get; set; }
        //public string Admin123Id { get; set; }
        public int NumberofMembers { get; set; }
        public decimal LastInvoice { get; set; }
        public long? BrokerId { get; set; }
        public string GroupLabel { get; set; }
        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        //public string Company { get; set; }
        //public string Phone { get; set; }
        //public string AlternatePhone { get; set; }
        //public string Fax { get; set; }
        //public string EmailId { get; set; }
        //public string AlternateEmailId { get; set; }
        //public bool? IsActive { get; set; }
        //public DateTime? ActiveDate { get; set; }
        //public DateTime? InactiveDate { get; set; }
        public DateTime? RenewalDate { get; set; }
    }
}
