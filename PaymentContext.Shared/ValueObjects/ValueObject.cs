using Flunt.Notifications;

namespace PaymentContext.Shared.ValueObjects
{
    public abstract class ValueObject : Notifiable<Notification>
    {
        public virtual void Validate() { } 
    }
}