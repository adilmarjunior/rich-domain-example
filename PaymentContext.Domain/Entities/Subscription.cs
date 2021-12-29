using System;
using System.Collections.Generic;
using System.Linq;
using PaymentContext.Shared.Entities;

namespace PaymentContext.Domain.Entities
{
    public class Subscription : Entity
    {
        private IList<Payment> _payments;
        public DateTime CreateDate { get; private set; }
        public DateTime LastUpdateDate { get; private set; }
        public DateTime? ExpireDate { get; private set; }
        public bool Active { get; private set; }
        public IReadOnlyCollection<Payment> Payments { get { return _payments.ToArray(); } }
        
        public Subscription(DateTime? expireDate) : base()
        {
            CreateDate = DateTime.Now;
            LastUpdateDate = DateTime.Now;
            ExpireDate = expireDate;
            Active = true;

            _payments = new List<Payment>();
        }

        public void AddPayment(Payment payment)
        {
            if (payment.PaidDate > DateTime.Now)
                AddNotification("Payment", "A data do pagamento deve ser futura");

            if (IsValid)
                _payments.Add(payment);
        }

        public void SetActive(bool active)
        {
            Active = active;
            LastUpdateDate = DateTime.Now;
        }
    }
}