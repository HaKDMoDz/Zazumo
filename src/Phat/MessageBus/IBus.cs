using System;
namespace Phat
{
    public enum Priority
    {
        High,
        Medium,
        Low
    }

    public interface IBus
    {
        void Publish(Object message);
        void Publish(Object message, Boolean isImmediate);

        void Subscribe<TMessage>(Action<TMessage> action, Object subscriber);
        void Subscribe<TMessage>(Action<TMessage> action, Object subscriber, Priority priority);
        void Subscribe(Type messageType, Delegate action, Object subscriber, Priority priority);

        void Unsubscribe(Object subscriber);
    }
}
