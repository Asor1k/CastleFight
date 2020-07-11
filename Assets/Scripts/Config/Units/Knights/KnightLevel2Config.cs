using System.Collections;
using System.Collections.Generic;
using CastleFight.Core;
using UnityEngine;

namespace CastleFight.Config
{
    [CreateAssetMenu(fileName = "KnightLevel2", menuName = "Units/KnightLevel2", order = 0)]
    public class KnightLevel2Config : BaseUnitConfig
    {
        public override Unit Create(Team team)
        {
            var unit = Pool.I.Get<KnightLevel2>();

            if(unit == null)
            {
                unit = Instantiate(prefab) as KnightLevel2;
                unit.Init(this, team);
                unit.gameObject.SetActive(true);
            }

            unit.gameObject.layer = (int)team;
            return unit;
        }
    }
}