using System.Collections.Generic;
using System.Linq;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests.Mocks
{
    public class FakeStudentRepository : IStudentRepository
    {
        private readonly List<Student> _students = new()
        {
            new Student(new Name("Bruce", "Waine"), new Document("123456", Domain.Enums.EDocumentType.CPF), new Email("bruce@waine.com")),
            new Student(new Name("Linus", "Tovalds"), new Document("654321", Domain.Enums.EDocumentType.CPF), new Email("linus@tovalds.com")),
            new Student(new Name("Bill", "Gates"), new Document("123654", Domain.Enums.EDocumentType.CPF), new Email("bill@gates.com")),
        };

        public void CreateSubscription(Student student)
        {
            _students.Add(student);
        }

        public bool DocumentExists(string document)
        {
            return _students.Where(_ => _.Document.Number == document).FirstOrDefault() != null;
        }

        public bool EmailExists(string email)
        {
            return _students.Where(_ => _.Email.Address == email).FirstOrDefault() != null;
        }

        public IQueryable<Student> GetAll()
        {
            return _students.AsQueryable();
        }
    }
}