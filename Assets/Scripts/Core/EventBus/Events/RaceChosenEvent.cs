using CastleFight.Core.EventsBus.Events;

namespace CastleFight
{
    public class RaceChosenEvent : EventBase
    {
        public RaceConfig UserRaceConfig { get; private set; }

        public void SetUserConfig(RaceConfig config)
        {
            UserRaceConfig = config;
        }
    }
}