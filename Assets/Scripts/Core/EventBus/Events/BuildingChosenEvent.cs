using CastleFight.Config;

namespace CastleFight.Core.EventsBus.Events
{
    public class BuildingChosenEvent : EventBase
    {
        public readonly BaseBuildingConfig buildingConfig;

        public BuildingBehavior GetBehavior => buildingConfig.Create().Behavior;

        public BuildingChosenEvent(BaseBuildingConfig buildingConfig)
        {
            this.buildingConfig = buildingConfig;
        }
    }
}