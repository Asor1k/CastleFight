namespace CastleFight.Core.EventsBus.Events
{
    public class BuildingClickedEvent:EventBase
    {
        public readonly BuildingBehavior behavior;

        public BuildingClickedEvent(BuildingBehavior behavior)
        {
            this.behavior = behavior;
        }
    }
}