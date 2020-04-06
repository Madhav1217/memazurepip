using System;
using System.Collections.Generic;
using System.Text;

namespace Aliera.BusinessObjects.Auth
{
   public class QuestionsAndAnswersBO
    {
        public string Username { get; set; }
        public List<QuestionAnswerBO> QuestionsAndAnswers { get; set; }
        public int PortalId { get; set; }
    }
}
