namespace Aliera.BusinessObjects.Broker
{
    public class CardValidationBO
    {
        public string CardType { get; set; }
        public string[] CardMaxLength { get; set; }
        public string[] CardStartingDigits { get; set; }
    }
}
