using System.Collections;
using System.Collections.Generic;
using CastleFight.Core;
using UnityEngine;

namespace CastleFight.Config
{
    [CreateAssetMenu(fileName = "KnightLevel1", menuName = "Units/KnightLevel1", order = 0)]
    public class KnightLevel1Config : BaseUnitConfig
    {
        public override Unit Create(Team team)
        {
            var unit = Pool.I.Get<KnightLevel1>();

            if(unit == null)
            {
                unit = Instantiate(prefab) as KnightLevel1;
                unit.Init(this, team);
                unit.gameObject.SetActive(true);
            }

            unit.gameObject.layer = (int)team;
            return unit;
        }
    }
}