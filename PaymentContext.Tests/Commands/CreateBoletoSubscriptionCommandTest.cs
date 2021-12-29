using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Commands;

namespace PaymentContext.Tests.Commands
{
    [TestClass]
    public class CreateBoletoSubscriptionCommandTest
    {
        [TestMethod]
        [DataTestMethod]
        [DataRow("")]
        [DataRow("a")]
        [DataRow("ab")]
        [DataRow("abc")]
        public void Should_Return_Error_When_Name_Invalid(string name)
        {
            var command = new CreateBoletoSubscriptionCommand { FirstName = name };

            command.Validate();
            Assert.AreEqual(command.IsValid, false);
        }
    }
}