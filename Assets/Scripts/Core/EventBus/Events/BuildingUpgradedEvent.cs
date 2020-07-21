using CastleFight;
using UnityEngine;

namespace CastleFight.Core.EventsBus.Events
{
    public class BuildingUpgradedEvent : EventBase
    {
        public Building building { get; private set; }
        
        public BuildingUpgradedEvent (Building building)
        {
            this.building = building;
        }
    }
}