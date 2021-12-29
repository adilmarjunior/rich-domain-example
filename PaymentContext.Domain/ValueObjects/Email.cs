using PaymentContext.Domain.ValueObjects.ValidationContracts;
using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjects
{
    public class Email : ValueObject
    {
        public string Address { get; set; }
        public Email(string address)
        {
            Address = address;
        }

        public override void Validate()
        {
            AddNotifications(new EmailContract(this));
        }
    }
}