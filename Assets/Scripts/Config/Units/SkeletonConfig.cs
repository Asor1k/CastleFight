using UnityEngine;
using CastleFight.Core;

namespace CastleFight.Config
{
    [CreateAssetMenu(fileName = "Skeleton", menuName = "Units/Skeleton", order = 0)]
    public class SkeletonConfig : BaseUnitConfig
    {
        public override Unit Create(Team team)
        {
            var unit = Pool.I.Get<Skeleton>();

            if(unit == null)
            {
                unit = Instantiate(prefab) as Skeleton;
                unit.Init(this, team);
                unit.gameObject.SetActive(true);
            }

            unit.gameObject.layer = (int)team;
            return unit;
        }
    }
}