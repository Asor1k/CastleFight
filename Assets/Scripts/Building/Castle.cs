using CastleFight.Core.EventsBus;
using CastleFight.Core.EventsBus.Events;

namespace CastleFight
{
    public class Castle : Building
    {
        private void Awake()
        {
            EventBusController.I.Bus.Publish(new BuildingPlacedEvent(Behavior));
        }
    }
}