using Flunt.Validations;

namespace PaymentContext.Domain.ValueObjects.ValidationContracts
{
    public class NameContract : Contract<Name>
    {
        public NameContract(Name name)
        {
            Requires()
                .IsLowerThan(name.FirstName, 40, nameof(name.FirstName), "Name should have no more than 40 chars")
                .IsGreaterThan(name.FirstName, 3, nameof(name.FirstName), "Name should have at least 3 chars")
                .IsGreaterThan(name.LastName, 3, nameof(name.LastName), "LastName should have at least 3 chars");
        }
    }
}