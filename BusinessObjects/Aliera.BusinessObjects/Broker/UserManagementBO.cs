using Aliera.Utilities.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aliera.BusinessObjects.Broker
{
    public class UserManagementBO
    {
        public string UserName { get; set; }
        public long UserId { get; set; }
        public EntityType EntityType { set; get; }
    }
}
