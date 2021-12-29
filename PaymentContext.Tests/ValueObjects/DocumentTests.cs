using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests.ValueObjects
{
    [TestClass]
    public class DocumentTests
    {
        // Red, Green, Refactor

        [TestMethod]
        [DataTestMethod]
        [DataRow("123")]
        [DataRow("1234")]
        [DataRow("1234567891011121345456")]
        [DataRow("1234567891011121345")]
        public void Should_Return_Error_When_CNPJ_Invalid(string cnpj)
        {
            var doc = new Document(cnpj, EDocumentType.CPNJ);
            Assert.IsTrue(!doc.IsValid);
        }

        [TestMethod]
        public void Should_Return_Success_When_CNPJ_Valid()
        {
            var doc = new Document("12345678911223", EDocumentType.CPNJ);
            Assert.IsTrue(doc.IsValid);
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow("123")]
        [DataRow("1234")]
        [DataRow("1234567891011121345456")]
        [DataRow("1234567891011121345")]
        public void Should_Return_Error_When_CPF_Invalid(string cpf)
        {
            var doc = new Document("123", EDocumentType.CPF);
            Assert.IsTrue(!doc.IsValid);
        }

        [TestMethod]
        public void Should_Return_Success_When_CPF_Valid()
        {
            var doc = new Document("12345678911", EDocumentType.CPF);
            Assert.IsTrue(doc.IsValid);
        }
    }
}