using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CastleFight
{
    public class Building : BuildingBehavior
    {
        private Unit _unit;

        public void Init(Unit unit)
        {
            _unit = unit;
        }

        public override void MoveTo(Vector3 position)
        {
            transform.position = position;
        }
    }
}
