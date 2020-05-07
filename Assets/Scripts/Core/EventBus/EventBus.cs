using System;
using System.Collections.Generic;
using System.Linq;
using CastleFight.Core.EventsBus.Events;
using CastleFight.Core.EventsBus.Interfaces;

namespace CastleFight.Core.EventsBus
{
    /// <summary>
    /// Implements <see cref="IEventBus"/>.
    /// </summary>
    public class EventBus : IEventBus
    {
        private readonly Dictionary<Type, List<ISubscription>> _subscriptions;

        // private readonly Dictionary<object, Type> typeMap;
        private readonly List<TypePair> typeMap;
        private static readonly object SubscriptionsLock = new object();

        private class TypePair
        {
            public object method;
            public Type type;
        }

        public EventBus()
        {
            _subscriptions = new Dictionary<Type, List<ISubscription>>();
            // typeMap = new Dictionary<object, Type>();
            typeMap = new List<TypePair>();
        }


        /// <summary>
        /// Subscribes to the specified event type with the specified action
        /// </summary>
        /// <typeparam name="TEventBase">The type of event</typeparam>
        /// <param name="action">The Action to invoke when an event of this type is published</param>
        /// <returns>A <see cref="SubscriptionToken"/> to be used when calling <see cref="Unsubscribe"/></returns>
        public void Subscribe<TEventBase>(Action<TEventBase> action) where TEventBase : EventBase
        {
            if (action == null)
                throw new ArgumentNullException("action");

            lock (SubscriptionsLock)
            {
                if (!_subscriptions.ContainsKey(typeof(TEventBase)))
                    _subscriptions.Add(typeof(TEventBase), new List<ISubscription>());

                typeMap.Add(new TypePair {method = action, type = typeof(TEventBase)});
                _subscriptions[typeof(TEventBase)].Add(new Subscription<TEventBase>(action));
            }
        }

        /// <summary>
        /// Unsubscribe from the Event type related to the specified <see cref="SubscriptionToken"/>
        /// </summary>
        /// <param name="token">The <see cref="SubscriptionToken"/> received from calling the Subscribe method</param>
        public void Unsubscribe<TEventBase>(Action<TEventBase> action) where TEventBase : EventBase
        {
            if (action == null)
                throw new ArgumentNullException("action");

            lock (SubscriptionsLock)
            {
                Type type = typeMap.FirstOrDefault(pair => pair.method.Equals(action))?.type;
                if (_subscriptions.ContainsKey(type))
                {
                    var allSubscriptions = _subscriptions[type];

                    ISubscription subscriptionToRemove = null;
                    foreach (var x in allSubscriptions)
                    {
                        if (Equate(x.SubscriptionToken as Action<TEventBase>, action))
                        {
                            subscriptionToRemove = x;
                            break;
                        }
                    }

                    if (subscriptionToRemove != null)
                        _subscriptions[type].Remove(subscriptionToRemove);
                }
            }
        }
        
        private bool Equate(System.Delegate a, System.Delegate b)
        {
            // ADDED THIS --------------
            // remove delegate overhead
            while (a.Target is Delegate)
                a = a.Target as Delegate;
            while (b.Target is Delegate)
                b = b.Target as Delegate;

            // standard equality
            if (a == b)
                return true;

            // null
            if (a == null || b == null)
                return false;

            // compiled method body
            if (a.Target != b.Target)
                return false;
            byte[] a_body = a.Method.GetMethodBody().GetILAsByteArray();
            byte[] b_body = b.Method.GetMethodBody().GetILAsByteArray();
            if (a_body.Length != b_body.Length)
                return false;
            for (int i = 0; i < a_body.Length; i++)
            {
                if (a_body[i] != b_body[i])
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Publishes the specified event to any subscribers for the <see cref="TEventBase"/> event type
        /// </summary>
        /// <typeparam name="TEventBase">The type of event</typeparam>
        /// <param name="eventItem">Event to publish</param>
        public void Publish<TEventBase>(TEventBase eventItem) where TEventBase : EventBase
        {
            if (eventItem == null)
                throw new ArgumentNullException("eventItem");

            List<ISubscription> allSubscriptions = new List<ISubscription>();
            lock (SubscriptionsLock)
            {
                if (_subscriptions.ContainsKey(typeof(TEventBase)))
                    allSubscriptions = _subscriptions[typeof(TEventBase)];
            }

            foreach (var subscription in allSubscriptions)
            {
                subscription.Publish(eventItem);
            }
        }

        /// <summary>
        /// Publishes the specified event to any subscribers for the <see cref="TEventBase"/> event type asychronously
        /// </summary>
        /// <remarks> This is a wrapper call around the synchronous  method as this method is naturally synchronous (CPU Bound) </remarks>
        /// <typeparam name="TEventBase">The type of event</typeparam>
        /// <param name="eventItem">Event to publish</param>
        public void PublishAsync<TEventBase>(TEventBase eventItem) where TEventBase : EventBase
        {
            PublishAsyncInternal(eventItem, null);
        }

        /// <summary>
        /// Publishes the specified event to any subscribers for the <see cref="TEventBase"/> event type asychronously
        /// </summary>
        /// <remarks> This is a wrapper call around the synchronous  method as this method is naturally synchronous (CPU Bound) </remarks>
        /// <typeparam name="TEventBase">The type of event</typeparam>
        /// <param name="eventItem">Event to publish</param>
        /// <param name="callback"><see cref="AsyncCallback"/> that is called on completion</param>
        public void PublishAsync<TEventBase>(TEventBase eventItem, AsyncCallback callback) where TEventBase : EventBase
        {
            PublishAsyncInternal(eventItem, callback);
        }

        #region PRIVATE METHODS

        private void PublishAsyncInternal<TEventBase>(TEventBase eventItem, AsyncCallback callback) where TEventBase : EventBase
        {
            Action publishAction = () => Publish(eventItem);
            publishAction.BeginInvoke(callback, null);
        }

        #endregion
    }
}