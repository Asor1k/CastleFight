using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CastleFight.Core;

namespace CastleFight.Config
{
    [CreateAssetMenu(fileName = "Skeleton", menuName = "Units/SkeletonLvl3", order = 0)]
    public class SkeletonLvl3Config : BaseUnitConfig
    {
        public override Unit Create(Team team)
        {
            var unit = Pool.I.Get<SkeletonLvl3>();

            if(unit == null)
            {
                unit = Instantiate(prefab) as SkeletonLvl3;
                unit.Init(this, team);
                unit.gameObject.SetActive(true);
            }

            unit.gameObject.layer = (int)team;
            return unit;
        }
    }
}
