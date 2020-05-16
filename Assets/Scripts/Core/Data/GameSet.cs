namespace CastleFight.Core.Data
{
    public class GameSet
    {
        public readonly RaceConfig userRaceConfig;
        public readonly RaceConfig botRaceConfig;

        public GameSet(RaceConfig userRaceConfig, RaceConfig botRaceConfig)
        {
            this.userRaceConfig = userRaceConfig;
            this.botRaceConfig = botRaceConfig;
        }
    }
}