using System;
using System.Collections.Generic;
using System.Text;

namespace Aliera.Utilities.Notifications
{
    public interface ISMSService
    {
        bool SendMessage(SMS sms);
    }
}
