using System;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Domain.Entities
{
    public class CreditCardPayment : Payment
    {
        public string CardHolderName { get; private set; }
        public string CardLastNumbers { get; private set; }
        public string LastTransactionNumber { get; private set; }

        public CreditCardPayment(string cardHolderName, string cardLastNumbers, string lastTransactionNumber, DateTime paidDate, DateTime expireDate, decimal total, decimal totalPaid, string owner, Document document, Address address, Email email) 
        : base(paidDate, expireDate, total, totalPaid, owner, document, address, email)
        {
            CardHolderName = cardHolderName;
            CardLastNumbers = cardLastNumbers;
            LastTransactionNumber = lastTransactionNumber;
        }
    }
}