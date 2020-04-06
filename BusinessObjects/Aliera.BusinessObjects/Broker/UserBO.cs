using System;
using Aliera.Utilities.Enumerations;

namespace Aliera.BusinessObjects.Broker
{
    public class UserBO
    {
        public long UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public UserStatus Status { get; set; }
        public string LastLoggedIn { get; set; }
        public int SessionIdleTime { get; set; }
        public DateTime ServerDateTime { get; set; }
    }
}
