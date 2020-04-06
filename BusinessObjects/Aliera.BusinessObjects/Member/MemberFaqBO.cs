using System;
using System.Collections.Generic;
using System.Text;

namespace Aliera.BusinessObjects.Member
{
    public class MemberFaqBO
    {
        public int Faqid { get; set; }
        public int FaqcatgId { get; set; }
        public string Faqquestion { get; set; }
        public string Faqanswer { get; set; }
    }
}
