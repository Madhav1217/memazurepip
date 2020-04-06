using System;
using System.Collections.Generic;

namespace Aliera.DatabaseEntities.Models
{
    public partial class UserSecurityQuestionAnswers
    {
        public long UserId { get; set; }
        public int SecurityQuestionId { get; set; }
        public string Answer { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long Id { get; set; }

        public virtual SecurityQuestions SecurityQuestion { get; set; }
        public virtual Users User { get; set; }
    }
}
