using System;
using Flunt.Notifications;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.Services;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Commands;
using PaymentContext.Shared.Handlers;

namespace PaymentContext.Domain.Handlers
{
    public class SubscriptionHandler :
        Notifiable<Notification>,
        IHandler<CreateBoletoSubscriptionCommand>,
        IHandler<CreateCreditCardSubscriptionCommand>,
        IHandler<CreatePayPalSubscriptionCommand>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IEmailService _emailService;

        public SubscriptionHandler(
            IStudentRepository studentRepository,
            IEmailService emailService
        )
        {
            _studentRepository = studentRepository;
            _emailService = emailService;
        }

        public ICommandResult Handle(CreateBoletoSubscriptionCommand command)
        {
            command.Validate();

            if (!command.IsValid)
            {
                AddNotifications(command);
                return new CommandResult(false, "Não foi possível realizar cadastro.");
            } 

            if (_studentRepository.DocumentExists(command.Document))
            {
                AddNotification(nameof(command.Document), "Documento já está em uso.");
                return new CommandResult(false, "Documento já cadastrado.");
            }

            if (_studentRepository.EmailExists(command.Email))
            {
                AddNotification(nameof(command.Email), "Email já está em uso.");
                return new CommandResult(false, "Email já cadastrado.");
            }

            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, command.PayerDocumentType);
            var email = new Email(command.Email);
            var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode);
            
            var student = new Student(name, document, email);
            
            var subscription = new Subscription(DateTime.Now.AddMonths(1));

            var payment = new BoletoPayment(command.BarCode, command.BoletoNumber, command.PaidDate, command.ExpireDate, command.Total, command.TotalPaid, command.Owner, new Document(command.PayerDocument, command.PayerDocumentType), address, email);

            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            AddNotifications(name, document, email, address, student, subscription, payment);

            if (!IsValid) return new CommandResult(false, "One or more informations are not valid.");

            _studentRepository.CreateSubscription(student);

            _emailService.Send(student.Name.ToString(), student.Email.Address, "Subscription", "Congrats, your subscription was finished.");

            return new CommandResult(true, "Subscription was created.");
        }

        public ICommandResult Handle(CreateCreditCardSubscriptionCommand command)
        {
            if (_studentRepository.DocumentExists(command.Document))
            {
                AddNotification(nameof(command.Document), "Documento já está em uso.");
                return new CommandResult(false, "Documento já cadastrado.");
            }

            if (_studentRepository.EmailExists(command.Email))
            {
                AddNotification(nameof(command.Email), "Email já está em uso.");
                return new CommandResult(false, "Email já cadastrado.");
            }

            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, command.PayerDocumentType);
            var email = new Email(command.Email);
            var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode);
            
            var student = new Student(name, document, email);
            
            var subscription = new Subscription(DateTime.Now.AddMonths(1));

            var payment = new CreditCardPayment(command.CardHolderName, command.CardLastNumbers, command.LastTransactionNumber, command.PaidDate, command.ExpireDate, command.Total, command.TotalPaid, command.Owner, new Document(command.PayerDocument, command.PayerDocumentType), address, email);

            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            AddNotifications(name, document, email, address, student, subscription, payment);

            if (!IsValid) return new CommandResult(false, "One or more informations are not valid.");

            _studentRepository.CreateSubscription(student);

            _emailService.Send(student.Name.ToString(), student.Email.Address, "Subscription", "Congrats, your subscription was finished.");

            return new CommandResult(true, "Subscription was created.");
        }

        public ICommandResult Handle(CreatePayPalSubscriptionCommand command)
        {
            if (_studentRepository.DocumentExists(command.Document))
            {
                AddNotification(nameof(command.Document), "Documento já está em uso.");
                return new CommandResult(false, "Documento já cadastrado.");
            }

            if (_studentRepository.EmailExists(command.Email))
            {
                AddNotification(nameof(command.Email), "Email já está em uso.");
                return new CommandResult(false, "Email já cadastrado.");
            }

            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, command.PayerDocumentType);
            var email = new Email(command.Email);
            var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode);
            
            var student = new Student(name, document, email);
            
            var subscription = new Subscription(DateTime.Now.AddMonths(1));

            var payment = new PayPalPayment(command.PaymentNumber, command.PaidDate, command.ExpireDate, command.Total, command.TotalPaid, command.Owner, new Document(command.PayerDocument, command.PayerDocumentType), address, email);

            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            AddNotifications(name, document, email, address, student, subscription, payment);

            if (!IsValid) return new CommandResult(false, "One or more informations are not valid.");

            _studentRepository.CreateSubscription(student);

            _emailService.Send(student.Name.ToString(), student.Email.Address, "Subscription", "Congrats, your subscription was finished.");

            return new CommandResult(true, "Subscription was created.");
        }
    }
}