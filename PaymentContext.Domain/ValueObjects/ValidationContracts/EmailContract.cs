using Flunt.Validations;

namespace PaymentContext.Domain.ValueObjects.ValidationContracts
{
    public class EmailContract : Contract<Email>
    {
        public EmailContract(Email email)
        {
            Requires()
                .IsEmail(email.Address, "Email", "Email inválido");
        }
    }
}