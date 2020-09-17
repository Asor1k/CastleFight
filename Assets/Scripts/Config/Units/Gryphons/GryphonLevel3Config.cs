using CastleFight.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CastleFight.Config
{
    [CreateAssetMenu(fileName = "GryphonLevel3", menuName = "Units/GryphonLevel3", order = 0)]
    public class GryphonLevel3Config : BaseUnitConfig
    {
        public override Unit Create(Team team)
        {
            var unit = Pool.I.Get<GryphonLevel3>();

            if (unit == null)
            {
                unit = Instantiate(prefab) as GryphonLevel3;
                unit.Init(this, team);
                unit.gameObject.SetActive(true);
            }

            unit.gameObject.layer = (int)team;
            return unit;
        }
    }
}