using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CastleFight.Core.EventsBus.Events;

namespace CastleFight
{
    public class BuildingDestroyedEvent : EventBase
    {
        public Building building { get; private set; }

        public BuildingDestroyedEvent(Building building)
        {
            this.building = building;
        }
    }
}