using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjects
{
    public class Name : ValueObject
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public override void Validate()
        {
            if (string.IsNullOrEmpty(FirstName))
                AddNotification("FirstName", "Nome inválido");
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}