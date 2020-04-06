using System;
using System.Collections.Generic;

namespace Aliera.DatabaseEntities.Models
{
    public partial class SecurityQuestions
    {
        public SecurityQuestions()
        {
            UserSecurityQuestionAnswers = new HashSet<UserSecurityQuestionAnswers>();
        }

        public int SecurityQuestionId { get; set; }
        public string SecurityQuestion { get; set; }

        public virtual ICollection<UserSecurityQuestionAnswers> UserSecurityQuestionAnswers { get; set; }
    }
}
