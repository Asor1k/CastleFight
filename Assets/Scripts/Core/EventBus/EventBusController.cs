namespace CastleFight.Core.EventsBus
{
    public class EventBusController
    {
        public static EventBusController I => instance ?? (instance = new EventBusController());

        private static EventBusController instance;

        public EventBus Bus { get; }

        private EventBusController()
        {
            Bus = new EventBus();
        }
    }
}