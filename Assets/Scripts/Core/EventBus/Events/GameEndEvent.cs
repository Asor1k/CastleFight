namespace CastleFight.Core.EventsBus.Events
{
    public class GameEndEvent : EventBase
    {
        public readonly Team loserTeam;
        public readonly Race loserRace;
        public GameEndEvent (Team loserTeam, Race race)
        {
            this.loserTeam = loserTeam;
            loserRace = race;
        }
    }
}