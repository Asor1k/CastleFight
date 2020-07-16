using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CastleFight.Core;

namespace CastleFight.Config
{
    [CreateAssetMenu(fileName = "Skeleton", menuName = "Units/SkeletonLvl2", order = 0)]
    public class SkeletonLvl2Config : BaseUnitConfig
    {
        public override Unit Create(Team team)
        {
            var unit = Pool.I.Get<SkeletonLvl2>();

            if(unit == null)
            {
                unit = Instantiate(prefab) as SkeletonLvl2;
                unit.Init(this, team);
                unit.gameObject.SetActive(true);
            }

            unit.gameObject.layer = (int)team;
            return unit;
        }
    }
}
