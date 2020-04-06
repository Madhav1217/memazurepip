using System;
using System.Collections.Generic;
using System.Text;

namespace Aliera.BusinessObjects.Broker
{
   public class UserSecurityQuestionsBO
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public int QuestionId { get; set; }
        public string Answer { get; set; }
    }
}
