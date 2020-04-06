namespace Aliera.BusinessObjects.Broker
{
    public class CreditCardBO
    {
        public int CCTYPE { get; set; }
        public string CCNUMBER { get; set; }
        public int CCEXPMONTH { get; set; } = 0;
        public int CCEXPYEAR { get; set; } = 0;
        public int CCSECURITYCODE { get; set; } = 0;
        public string NameOnCard { get; set; }
    }
}
