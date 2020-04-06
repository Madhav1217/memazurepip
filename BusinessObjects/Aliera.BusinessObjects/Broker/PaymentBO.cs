using System.Collections.Generic;

namespace Aliera.BusinessObjects.Broker
{
    public class PaymentBO
    {
        public int PaymentType { get; set; }
        public ACHBankDraftBO ACHBankDraft { get; set; }
        public CreditCardBO CreditCard { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
        public List<CardValidationBO> CardValidation { get; set; }
    }
}
