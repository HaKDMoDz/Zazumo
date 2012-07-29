using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Diagnostics;

namespace Phat.MessageBus
{
    public class Bus : IBus, IQueueingBus
    {
        private readonly IDictionary<Type, IList<Subscription>> _subscriptions;
        private readonly Queue<QueuedAction> _highPriorityQueue;
        private readonly Queue<QueuedAction> _mediumPriorityQueue;
        private readonly Queue<QueuedAction> _lowPriorityQueue;
        private readonly Queue<QueuedAction> _immediateQueue;

        private Object lockObject;


        public Bus()
        {
            lockObject = new Object();
            _subscriptions = new Dictionary<Type, IList<Subscription>>();
            _highPriorityQueue = new Queue<QueuedAction>();
            _mediumPriorityQueue = new Queue<QueuedAction>();
            _lowPriorityQueue = new Queue<QueuedAction>();
            _immediateQueue = new Queue<QueuedAction>();
        }

        public void Publish(Object message)
        {
            this.Publish(message, false);
        }

        public void Publish(Object message, Boolean isImmediate)
        {
            lock (lockObject)
            {
                if (!_subscriptions.ContainsKey(message.GetType()))
                    return;

                foreach (var subscription in _subscriptions[message.GetType()])
                {
                    if (isImmediate)
                    {
                        _immediateQueue.Enqueue(new QueuedAction(subscription.Action, message));
                    }
                    else
                    {
                        switch (subscription.Priority)
                        {
                            case Priority.High:
                                _highPriorityQueue.Enqueue(new QueuedAction(subscription.Action, message));
                                break;
                            case Priority.Medium:
                                _mediumPriorityQueue.Enqueue(new QueuedAction(subscription.Action, message));
                                break;
                            case Priority.Low:
                                _lowPriorityQueue.Enqueue(new QueuedAction(subscription.Action, message));
                                break;
                            default:
                                break;
                        }
                    }
                }
            }

            while (_immediateQueue.Count > 0)
            {
                var action = _immediateQueue.Dequeue();
                action.Action.DynamicInvoke(action.Message);
            }
        }

        public void Subscribe<TMessage>(Action<TMessage> action, Object subscriber)
        {
            Subscribe<TMessage>(action, subscriber, Priority.Medium);
        }        

        public void Subscribe<TMessage>(Action<TMessage> action, Object subscriber, Priority priority)
        {
            Subscribe(typeof(TMessage), action, subscriber, priority);
            /*lock (lockObject)
            {
                if (!_subscriptions.ContainsKey(typeof(TMessage)))
                    _subscriptions[typeof(TMessage)] = new List<Subscription>();

                var collection = _subscriptions[typeof(TMessage)];

                collection.Add(new Subscription(priority, action, typeof(TMessage), subscriber));
            }*/
        }

        public void Subscribe(Type messageType, Delegate action, object subscriber, Priority priority)
        {
            lock (lockObject)
            {
                if (!_subscriptions.ContainsKey(messageType))
                    _subscriptions[messageType] = new List<Subscription>();

                var collection = _subscriptions[messageType];

                collection.Add(new Subscription(priority, action, messageType, subscriber));
            }
        }

        public void Unsubscribe(Object subscriber)
        {
            foreach (var s in _subscriptions.Values)
            {
                for (Int32 index = s.Count - 1; index >= 0; index--)
                {
                    if (s[index].Subscriber == subscriber)
                        s.RemoveAt(index);
                }
            }
        }


        public void RunQueuedHandlers(Int32 timeoutInMilliseconds)
        {
            // Stopwatch sw = new Stopwatch();
            // sw.Start();

            while (_highPriorityQueue.Count > 0)
            {
                var action = _highPriorityQueue.Dequeue();
                action.Action.DynamicInvoke(action.Message);
            }

            //if (timeoutInMilliseconds)
                //Debug.WriteLine("High priority actions did not finish in time.");

            while (_mediumPriorityQueue.Count > 0)
            {
                var action = _mediumPriorityQueue.Dequeue();
                action.Action.DynamicInvoke(action.Message);
            }

            //if (sw.ElapsedMilliseconds > timeoutInMilliseconds)
               // Debug.WriteLine("Medium priority actions did not finish in time.");

            while (_lowPriorityQueue.Count > 0)
            {
                var action = _lowPriorityQueue.Dequeue();
                action.Action.DynamicInvoke(action.Message);
            }

            //sw.Stop();
            //if (sw.ElapsedMilliseconds > timeoutInMilliseconds)
                //Debug.WriteLine("Low priority actions did not finish in time.");
        }
    }
}
