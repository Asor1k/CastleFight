using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CastleFight.Core;

namespace CastleFight.Config
{
    [CreateAssetMenu(fileName = "Knight", menuName = "Units/Knight", order = 0)]
    public class KnightConfig : BaseUnitConfig
    {
        public override Unit Create(Team team)
        {
            var unit = Pool.I.Get<Knight>();

            if (unit == null)
            {
                unit = Instantiate(prefab) as Knight;
                unit.Init(this, team);
                unit.gameObject.SetActive(true);
            }

            unit.gameObject.layer = (int)team;
            return unit;
        }
    }
}
