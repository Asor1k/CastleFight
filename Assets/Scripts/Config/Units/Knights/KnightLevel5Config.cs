using System.Collections;
using System.Collections.Generic;
using CastleFight.Core;
using UnityEngine;

namespace CastleFight.Config
{
    [CreateAssetMenu(fileName = "KnightLevel5", menuName = "Units/KnightLevel5", order = 0)]
    public class KnightLevel5Config : BaseUnitConfig
    {
        public override Unit Create(Team team)
        {
            var unit = Pool.I.Get<KnightLevel5>();

            if(unit == null)
            {
                unit = Instantiate(prefab) as KnightLevel5;
                unit.Init(this, team);
                unit.gameObject.SetActive(true);
            }

            unit.gameObject.layer = (int)team;
            return unit;
        }
    }
}