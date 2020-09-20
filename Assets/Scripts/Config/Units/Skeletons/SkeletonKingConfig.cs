using CastleFight.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CastleFight.Config
{
    [CreateAssetMenu(fileName = "SkeletonKing", menuName = "Units/SkeletonKing", order = 0)]
    public class SkeletonKingConfig : BaseUnitConfig
    {
        public override Unit Create(Team team)
        {
            var unit = Pool.I.Get<SkeletonKing>();

            if (unit == null)
            {
                unit = Instantiate(prefab) as SkeletonKing;
                unit.Init(this, team);
                unit.gameObject.SetActive(true);
            }

            unit.gameObject.layer = (int)team;
            return unit;
        }
    }
}