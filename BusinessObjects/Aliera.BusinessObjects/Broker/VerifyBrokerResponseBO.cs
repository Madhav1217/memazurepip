using System;
using System.Collections.Generic;
using System.Text;

namespace Aliera.BusinessObjects.Broker
{
    public class VerifyBrokerResponseBO
    {
        public bool DoesBrokerExist { get; set; }

        public double ErrorCode { get; set; }

        public string ErrorMessage { get; set; }
    }
}
