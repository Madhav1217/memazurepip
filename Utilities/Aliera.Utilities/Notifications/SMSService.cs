using System;
using System.Collections.Generic;
using System.Text;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Aliera.Utilities.Notifications
{
    public class SMSService:ISMSService
    {
        public bool SendMessage(SMS sms)
        {
            try
            {
                TwilioClient.Init(sms.AccountSid, sms.AuthToken);
                var message = MessageResource.Create(
                    to: new PhoneNumber(sms.Number),
                    from: new PhoneNumber(sms.PhoneNumber),
                    body: sms.Text
                );
                
                return true;
            }
            catch
            {
                throw;
            }
        }
    }
}
