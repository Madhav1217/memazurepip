using System;
using System.Collections.Generic;
using System.Text;

namespace Aliera.Utilities.Notifications
{
    public class SMS
    {
        public string Number { get; set; }
        public string Text { get; set; }
        public string AccountSid { get; set; }
        public string AuthToken { get; set; }
        public string PhoneNumber { get; set; }
    }
}
