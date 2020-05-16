using CastleFight.Core.Data;

namespace CastleFight.Core.EventsBus.Events
{
    public class GameSetReadyEvent : EventBase
    {
        public readonly GameSet gameSet;

        public GameSetReadyEvent(RaceConfig userRaceConfig, RaceConfig botRaceConfig)
        {
            gameSet = new GameSet(userRaceConfig, botRaceConfig);
        }
    }
}