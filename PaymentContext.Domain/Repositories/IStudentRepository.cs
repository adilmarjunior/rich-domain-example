using System.Collections.Generic;
using System.Linq;
using PaymentContext.Domain.Entities;

namespace PaymentContext.Domain.Repositories
{
    public interface IStudentRepository
    {
        bool DocumentExists(string document);
        bool EmailExists(string email);
        void CreateSubscription(Student student);
        IQueryable<Student> GetAll();
    }
}