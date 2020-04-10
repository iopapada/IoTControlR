using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTControlR.Services
{
    public class MessageService : IMessageService
    {
        private object _sync = new Object();
        private List<Subscriber> _subscribers = new List<Subscriber>();
        public void Send<TSender, TArgs>(TSender sender, string message, TArgs args) where TSender : class
        {
            if (sender == null)
                throw new ArgumentNullException(nameof(sender));

            foreach (var subscriber in GetSubscribersSnapshot())
            {
                // Avoid sending message to self
                if (subscriber.Target != sender)
                {
                    subscriber.TryInvoke(sender, message, args);
                }
            }
        }

        private Subscriber[] GetSubscribersSnapshot()
        {
            lock (_sync)
            {
                return _subscribers.ToArray();
            }
        }

        class Subscriber
        {
            private WeakReference _reference = null;

            private Dictionary<Type, Subscriptions> _subscriptions;

            public Subscriber(object target)
            {
                _reference = new WeakReference(target);
                _subscriptions = new Dictionary<Type, Subscriptions>();
            }

            public object Target => _reference.Target;

            public bool IsEmpty => _subscriptions.Count == 0;

            public void AddSubscription<TSender, TArgs>(Action<TSender, string, TArgs> action)
            {
                if (!_subscriptions.TryGetValue(typeof(TSender), out Subscriptions subscriptions))
                {
                    subscriptions = new Subscriptions();
                    _subscriptions.Add(typeof(TSender), subscriptions);
                }
                subscriptions.AddSubscription(action);
            }

            public void RemoveSubscription<TSender>()
            {
                _subscriptions.Remove(typeof(TSender));
            }
            public void RemoveSubscription<TSender, TArgs>()
            {
                if (_subscriptions.TryGetValue(typeof(TSender), out Subscriptions subscriptions))
                {
                    subscriptions.RemoveSubscription<TArgs>();
                    if (subscriptions.IsEmpty)
                    {
                        _subscriptions.Remove(typeof(TSender));
                    }
                }
            }

            public void TryInvoke<TArgs>(object sender, string message, TArgs args)
            {
                var target = _reference.Target;
                if (_reference.IsAlive)
                {
                    var senderType = sender.GetType();
                    foreach (var keyValue in _subscriptions.Where(r => r.Key.IsAssignableFrom(senderType)))
                    {
                        var subscriptions = keyValue.Value;
                        subscriptions.TryInvoke(sender, message, args);
                    }
                }
            }
        }
        class Subscriptions
        {
            private Dictionary<Type, Delegate> _subscriptions = null;

            public Subscriptions()
            {
                _subscriptions = new Dictionary<Type, Delegate>();
            }

            public bool IsEmpty => _subscriptions.Count == 0;

            public void AddSubscription<TSender, TArgs>(Action<TSender, string, TArgs> action)
            {
                _subscriptions.Add(typeof(TArgs), action);
            }

            public void RemoveSubscription<TArgs>()
            {
                _subscriptions.Remove(typeof(TArgs));
            }

            public void TryInvoke<TArgs>(object sender, string message, TArgs args)
            {
                var argsType = typeof(TArgs);
                foreach (var keyValue in _subscriptions.Where(r => r.Key.IsAssignableFrom(argsType)))
                {
                    var action = keyValue.Value;
                    action?.DynamicInvoke(sender, message, args);
                }
            }
        }
    }
}
