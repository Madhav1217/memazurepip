using System;
using System.Collections.Generic;
using System.Text;

namespace Aliera.BusinessObjects.Auth
{
    public class UserSecurityQuestionBO
    {
        public long Id { get; set; }

        public long UserId { get; set; }

        public int QuestionId { get; set; }

        public string Answer { get; set; }
    }
}
