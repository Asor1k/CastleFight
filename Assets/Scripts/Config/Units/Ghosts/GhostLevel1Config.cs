using System.Collections;
using System.Collections.Generic;
using CastleFight.Core;
using UnityEngine;

namespace CastleFight.Config
{
    [CreateAssetMenu(fileName = "GhostLevel1", menuName = "Units/GhostLevel1", order = 0)]
    public class GhostLevel1Config : BaseUnitConfig
    {
        public override Unit Create(Team team)
        {
            var unit = Pool.I.Get<GhostLevel1>();

            if(unit == null)
            {
                unit = Instantiate(prefab) as GhostLevel1;
                unit.Init(this, team);
                unit.gameObject.SetActive(true);
            }

            unit.gameObject.layer = (int)team;
            return unit;
        }
    }
}