using System;
using Flunt.Validations;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Entities;

namespace PaymentContext.Domain.Entities
{
    public abstract class Payment : Entity
    {
        public string Number { get; private set; }
        public DateTime PaidDate { get; private set; }
        public DateTime ExpireDate { get; private set; }
        public decimal Total { get; private set; }
        public decimal TotalPaid { get; private set; }
        public string Owner { get; private set; }
        public Document Document { get; private set; }
        public Address Address { get; private set; }
        public Email Email { get; private set; }


        protected Payment(DateTime paidDate, DateTime expireDate, decimal total, decimal totalPaid, string owner, Document document, Address address, Email email)
        : base()
        {
            Number = Guid.NewGuid().ToString().Replace("-", "")[..10].ToUpper();
            PaidDate = paidDate;
            ExpireDate = expireDate;
            Total = total;
            TotalPaid = totalPaid;
            Owner = owner;
            Document = document;
            Address = address;
            Email = email;

            AddNotifications(new Contract<Payment>()
                .Requires()
                .IsLowerOrEqualsThan(0, Total, nameof(Total), "O valor total não pode ser zero")
                .IsGreaterOrEqualsThan(Total, TotalPaid, "TotalPaid", "O valor pago é menor que o valor do pagamento")
            );
        }
    }
}