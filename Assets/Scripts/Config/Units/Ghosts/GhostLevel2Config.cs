using System.Collections;
using System.Collections.Generic;
using CastleFight.Core;
using UnityEngine;

namespace CastleFight.Config
{
    [CreateAssetMenu(fileName = "GhostLevel2", menuName = "Units/GhostLevel2", order = 0)]
    public class GhostLevel2Config : BaseUnitConfig
    {
        public override Unit Create(Team team)
        {
            var unit = Pool.I.Get<GhostLevel2>();

            if(unit == null)
            {
                unit = Instantiate(prefab) as GhostLevel2;
                unit.Init(this, team);
                unit.gameObject.SetActive(true);
            }

            unit.gameObject.layer = (int)team;
            return unit;
        }
    }
}