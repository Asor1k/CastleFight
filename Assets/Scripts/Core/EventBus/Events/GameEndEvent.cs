namespace CastleFight.Core.EventsBus.Events
{
    public class GameEndEvent : EventBase
    {
        public readonly Team winnerTeam;
        public GameEndEvent (Team winnerTeam)
        {
            this.winnerTeam = winnerTeam;
        }
    }
}