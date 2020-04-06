using System;
using System.Collections.Generic;
using System.Text;

namespace Aliera.BusinessObjects.Member
{
    public class PaymentInformationBO
    {
        public string PaymentMethodType { get; set; }
        public string Last4Digits { get; set; }
        public string LastTransactionDate { get; set; }
        public decimal PremiumAmount { get; set; }

        //For Credit Card
        public string CreditCardNumber { get; set; }

        public string UnMaskedCreditCardNumber { get; set; }

        public string CardExpiryDate { get; set; }
        public string SecurityCode { get; set; }
        public string CreditCardType { get; set; }
        public int CreditCardExpiryYear { get; set; }
        public int CreditCardExpiryMonth { get; set; }

        //For ACH
        public string BankRoutingNumber { get; set; }

        public string BankAccountNumber { get; set; }
        public string UnMaskedBankRoutingNumber { get; set; }
        public string UnMaskedBankAccountNumber { get; set; }
        public string BankAccountType { get; set; }
        public string BankName { get; set; }

        //Member Details used for both CC and ACH as well as AutoFill
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }

        //Member Id for updating payment info for a member
        public long? MemberId { get; set; }

        public long UserId { get; set; }

        //Flag to identify for card or ACH update
        public bool IsCardUpdate { get; set; }

        public bool IsACHUpdate { get; set; }
    }
}