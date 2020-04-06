using System;
using System.Collections.Generic;
using System.Text;

namespace Aliera.BusinessObjects.RH
{
    public class RHPaymentInfoBO
    {
        public string MemberId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
        public string ACHAccountType { get; set; }       
        public int PaymentType { get; set; }
    }
}
