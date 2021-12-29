using System.Diagnostics.Contracts;
using Flunt.Validations;
using PaymentContext.Domain.Enums;

namespace PaymentContext.Domain.ValueObjects.ValidationContracts
{
    public class DocumentContract : Contract<Document>
    {
        public DocumentContract(Document document)
        {
            var qntChar = document.Type == EDocumentType.CPF ? 11 : 14;

            Requires()
                .IsTrue(document.Number.Length == qntChar, nameof(document.Number), $"O tamanho do documento deve ser {qntChar}");
        }
    }
}