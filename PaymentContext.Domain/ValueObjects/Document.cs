using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects.ValidationContracts;
using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjects
{
    public class Document : ValueObject
    {
        public string Number { get; set; }
        public EDocumentType Type { get; set; }

        public Document(string number, EDocumentType type)
        {
            Number = number;
            Type = type;

            Validate();
        }

        public override void Validate()
        {
            AddNotifications(new DocumentContract(this));
        }
    }
}