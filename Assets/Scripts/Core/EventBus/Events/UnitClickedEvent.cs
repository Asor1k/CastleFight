namespace CastleFight.Core.EventsBus.Events
{
    public class UnitClickedEvent : EventBase
    {
        public readonly UnitStats unit;
        public UnitClickedEvent(UnitStats unit)
        {
            this.unit = unit;
        }
    }
}