using System.Collections.Generic;
using System.Linq;
using Flunt.Validations;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Entities;

namespace PaymentContext.Domain.Entities
{
    public class Student : Entity
    {
        private IList<Subscription> _subscriptions;

        public Name Name { get; private set; }
        public Document Document { get; private set; }
        public Email Email { get; private set; }
        public Address Address { get; private set; }
        public IReadOnlyCollection<Subscription> Subscriptions { get { return _subscriptions.ToArray(); } }

        public Student(Name name, Document document, Email email) : base()
        {
            Name = name;
            Document = document;
            Email = email;
            _subscriptions = new List<Subscription>();

            AddNotifications(name, document, email);
        }
        
        public void AddSubscription(Subscription subscription)
        {
            var hasSubscriptionActive = false;

            foreach (var sub in _subscriptions)
            {
                if (sub.Active) 
                    hasSubscriptionActive = true;
            }

            AddNotifications(new Contract<Student>()
                .Requires()
                .IsGreaterThan(subscription.Payments.Count, 0, nameof(Payment), "Student must have at least one payment")
                .IsFalse(hasSubscriptionActive, nameof(Subscriptions), "Você já tem uma assinatura ativa")    
            );

            if (IsValid)
                _subscriptions.Add(subscription);
        }
    }
}