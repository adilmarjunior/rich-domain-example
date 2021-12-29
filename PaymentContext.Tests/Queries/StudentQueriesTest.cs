using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Queries;
using PaymentContext.Domain.Repositories;
using PaymentContext.Tests.Mocks;

namespace PaymentContext.Tests.Queries
{
    [TestClass]
    public class StudentQueriesTest
    {
        private readonly IStudentRepository _studentRepository;

        public StudentQueriesTest()
        {
            _studentRepository = new FakeStudentRepository();
        }

        [TestMethod]
        public void Should_Return_Null_When_Document_Not_Exists()
        {
            var query = StudentQueries.GetStudentByDocument("123456789");

            var student = _studentRepository.GetAll().Where(query).FirstOrDefault();

            Assert.AreEqual(student, null);
        }

        [TestMethod]
        public void Should_Return_Student_When_Document_Exists()
        {
            var query = StudentQueries.GetStudentByDocument("123456");

            var student = _studentRepository.GetAll().Where(query).FirstOrDefault();

            Assert.AreNotEqual(student, null);
        }
    }
}