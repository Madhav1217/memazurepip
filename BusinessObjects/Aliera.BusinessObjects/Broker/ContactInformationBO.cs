using System.Collections.Generic;
using Aliera.Utilities.Enumerations;

namespace Aliera.BusinessObjects.Broker
{
    public class ContactInformationBO
    {
        public string  OfficeLocations { get; set; }
        public List<string> PhoneNumber { get; set; }
        public string Email { get; set; }
        public string  MemberService { get; set; }
    }

}
