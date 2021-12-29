using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests.Entities
{
    [TestClass]
    public class StudentsTests
    {
        private readonly Name _name;
        private readonly Document _document;
        private readonly Email _email;
        private readonly Address _address;
        private readonly Student _student;
        private readonly Subscription _subscription;
        private readonly Payment _paypalPayment;

        public StudentsTests()
        {
            _name = new Name("Bruce", "Wayne");
            _document = new Document("12345678911", EDocumentType.CPF);
            _email = new Email("email@email.com");
            _address = new Address("Rua 1", "1234", "Bairro Legal", "Cidade Boa", "SP", "Brazil", "12345678");

            _subscription = new Subscription(null);
            _paypalPayment = new PayPalPayment("13654981321", DateTime.Now, DateTime.Now.AddDays(5), 10, 10, "WAYNE CORP", _document, _address, _email);

            _student = new Student(_name, _document, _email);
        }

        [TestMethod]
        public void Should_Return_Error_When_Active_Subscription()
        {
            _subscription.AddPayment(_paypalPayment);
            _student.AddSubscription(_subscription);
            _student.AddSubscription(_subscription);

            Assert.IsTrue(!_student.IsValid);
        }

        [TestMethod]
        public void Should_Return_Error_When_Subscription_Has_No_Payments()
        {
            _student.AddSubscription(_subscription);

            Assert.IsTrue(!_student.IsValid);
        }

        [TestMethod]
        public void Should_Return_Success_When_Has_No_Active_Subscription()
        {
            _subscription.AddPayment(_paypalPayment);
            _student.AddSubscription(_subscription);

            Assert.IsTrue(_student.IsValid);
        }
    }
}