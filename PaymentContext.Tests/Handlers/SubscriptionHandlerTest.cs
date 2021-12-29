using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Handlers;
using PaymentContext.Tests.Mocks;

namespace PaymentContext.Tests.Handlers
{
    [TestClass]
    public class SubscriptionHandlerTest
    {
        SubscriptionHandler _subscriptionHandler;

        public SubscriptionHandlerTest()
        {
            _subscriptionHandler = new SubscriptionHandler(
                new FakeStudentRepository(), 
                new FakeEmailService()
            );
        }

        [TestMethod]
        [DataTestMethod]
        [DataRow("123456")]
        [DataRow("123654")]
        [DataRow("654321")]
        public void Should_Return_Error_When_Document_Exists(string document)
        {
            var boletoCommand = new CreateBoletoSubscriptionCommand
            {
                BarCode = "123156498754654",
                BoletoNumber = "2132165151321321",
                City = "City",
                Country = "Country",
                Document = document,
                Email = "email@email.com",
                ExpireDate = DateTime.Now.AddDays(30),
                FirstName = "Bruce",
                LastName = "Waine",
                Neighborhood = "good neig",
                Number = "2135",
                Owner = "Bruce Wayne",
                PaidDate = DateTime.Now,
                PayerDocument = document,
                PayerDocumentType = Domain.Enums.EDocumentType.CPF,
                PayerEmail = "email@email.com",
                PaymentNumber = "123456789",
                State = "State",
                Street = "Street 4",
                Total = 100,
                TotalPaid = 100
            };

            var result = _subscriptionHandler.Handle(boletoCommand) as CommandResult;

            Assert.AreEqual(result.Success, false);
        }
    }
}