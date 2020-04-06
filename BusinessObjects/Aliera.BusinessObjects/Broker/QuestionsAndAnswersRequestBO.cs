using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Aliera.BusinessObjects.Broker
{
   public class QuestionsAndAnswersRequestBO
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public List<QuestionAndAnswerModel> QuestionsAndAnswers { get; set; }

        [Required]
        public int PortalId { get; set; }
    }
    public class QuestionAndAnswerModel
    {
        [Required]
        public int QuestionId { get; set; }

        [Required]
        [StringLength(100)]
        public string Answer { get; set; }
    }
}
