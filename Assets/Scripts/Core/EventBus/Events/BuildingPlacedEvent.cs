namespace CastleFight.Core.EventsBus.Events
{
    public class BuildingPlacedEvent : EventBase
    {
        public readonly BuildingBehavior behavior;

        public BuildingPlacedEvent(BuildingBehavior behavior)
        {
            this.behavior = behavior;
        }
    }
}