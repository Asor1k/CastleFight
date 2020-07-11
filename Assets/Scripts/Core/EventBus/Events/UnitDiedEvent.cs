using CastleFight.Core.EventsBus.Events;

namespace CastleFight
{
    public class UnitDiedEvent : EventBase
    {
        public Unit Unit { get; private set; }

        public UnitDiedEvent(Unit unit)
        {
            Unit = unit;
        }
    }
}