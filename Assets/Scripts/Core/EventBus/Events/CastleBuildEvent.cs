using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CastleFight.Core.EventsBus.Events
{
    public class CastleBuildEvent : EventBase
    {
        public Castle castle { get; private set; }

        public CastleBuildEvent(Castle castle)
        {
            this.castle = castle;
        }
    }
}