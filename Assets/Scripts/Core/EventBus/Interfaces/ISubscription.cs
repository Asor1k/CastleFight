using System;
using CastleFight.Core.EventsBus.Events;

namespace CastleFight.Core.EventsBus.Interfaces
{
    public interface ISubscription
    {
        /// <summary>
        /// Token returned to the subscriber
        /// </summary>
        object SubscriptionToken { get; }

        /// <summary>
        /// Publish to the subscriber
        /// </summary>
        /// <param name="eventBase"></param>
        void Publish(EventBase eventBase);
    }
}
