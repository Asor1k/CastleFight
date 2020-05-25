using CastleFight.Core.EventsBus.Events;

namespace CastleFight{
    public class UnitSpawnedEvent : EventBase
    {
        public Unit Unit {get; private set;}

        public UnitSpawnedEvent(Unit unit)
        {
            Unit = unit;
        }
    }
}