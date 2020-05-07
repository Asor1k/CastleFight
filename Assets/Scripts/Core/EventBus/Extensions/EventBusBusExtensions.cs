using System;
using CastleFight.Core.EventsBus.Events;
using CastleFight.Core.EventsBus.Interfaces;

namespace CastleFight.Core.EventsBus.Extensions
{
    public static class EventBusBusExtensions
    {
        public static void Publish(this EventBase eventBase, IEventBus eventBus)
        {
            eventBus.Publish(eventBase);
        }

        public static void PublishAsync(this EventBase eventBase, IEventBus eventBus)
        {
            eventBus.PublishAsync(eventBase);
        }

        public static void PublishAsync(this EventBase eventBase, IEventBus eventBus, AsyncCallback asyncCallback)
        {
            eventBus.PublishAsync(eventBase, asyncCallback);
        }

//        public static void Unsubscribe(this Action<EventBase> token, IEventBus eventBus)
//        {
//            eventBus.Unsubscribe(token);
//        }
    }
}
