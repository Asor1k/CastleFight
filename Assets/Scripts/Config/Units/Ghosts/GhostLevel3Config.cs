using CastleFight.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CastleFight.Config
{
    [CreateAssetMenu(fileName = "GhostLevel3", menuName = "Units/GhostLevel3", order = 0)]
    public class GhostLevel3Config : BaseUnitConfig
    {
        public override Unit Create(Team team)
        {
            var unit = Pool.I.Get<GhostLevel3>();

            if (unit == null)
            {
                unit = Instantiate(prefab) as GhostLevel3;
                unit.Init(this, team);
                unit.gameObject.SetActive(true);
            }

            unit.gameObject.layer = (int)team;
            return unit;
        }
    }
}