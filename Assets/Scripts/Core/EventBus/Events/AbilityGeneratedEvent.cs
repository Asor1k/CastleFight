using CastleFight.Config;
using CastleFight.Core.EventsBus;
using CastleFight.Core.EventsBus.Events;
using UnityEngine;

namespace CastleFight
{
    public class AbilityGeneratedEvent : EventBase
    {
        public Ability ability { get; private set; }
        public UnitKind unitKind { get; private set; }

        public AbilityGeneratedEvent(Ability ability, UnitKind unitKind)
        {
            this.ability = ability;
            this.unitKind = unitKind;
        }

    }
}