namespace CastleFight.Core.EventsBus.Events
{
    public class GameEndEvent : EventBase
    {
        public readonly bool won;
        public readonly Race loserRace;
        public GameEndEvent (Team loserTeam, Race race)
        {
            won = loserTeam == Team.Team2;
            loserRace = race;
        }
    }
}