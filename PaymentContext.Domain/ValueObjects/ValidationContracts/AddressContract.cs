using Flunt.Validations;

namespace PaymentContext.Domain.ValueObjects.ValidationContracts
{
    public class AddressContract : Contract<Address>
    {
        public AddressContract(Address address)
        {
            Requires()
                .IsGreaterThan(address.Number, 0, nameof(address.Number), "Number should be greater than zero")
                .IsGreaterThan(address.Street, 3, nameof(address.Street), "Street should have at least 3 chars")
                .IsGreaterThan(address.Neighborhood, 3, nameof(address.Neighborhood), "Neighborhood should have at least 3 chars")
                .IsGreaterThan(address.City, 3, nameof(address.City), "City should have at least 3 chars")
                .IsGreaterThan(address.State, 2, nameof(address.State), "State should have at least 2 chars");
        }
    }
}