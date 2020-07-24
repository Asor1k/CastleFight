using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CastleFight.Core;

namespace CastleFight.Config
{
    [CreateAssetMenu(fileName = "KnightLevel3", menuName = "Units/KnightLevel3", order = 0)]
    public class KnightLevel3Config : BaseUnitConfig
    {
        public override Unit Create(Team team)
        {
            var unit = Pool.I.Get<KnightLevel3>();

            if(unit == null)
            {
                unit = Instantiate(prefab) as KnightLevel3;
                unit.Init(this, team);
                unit.gameObject.SetActive(true);
            }

            unit.gameObject.layer = (int)team;
            return unit;
        }
    }
}