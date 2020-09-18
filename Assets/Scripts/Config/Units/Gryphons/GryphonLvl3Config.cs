using System.Collections;
using System.Collections.Generic;
using CastleFight.Config;
using CastleFight.Core;
using UnityEngine;

namespace CastleFight
{
    [CreateAssetMenu(fileName = "GryphonLevel3", menuName = "Units/GryphonLevel3", order = 0)]
    public class GryphonLvl3Config : BaseUnitConfig
    {
            public override Unit Create(Team team)
            {
                var unit = Pool.I.Get<GryphonLvl3>();

                if (unit == null)
                {
                    unit = Instantiate(prefab) as GryphonLvl3;
                    unit.Init(this, team);
                    unit.gameObject.SetActive(true);
                }

                unit.gameObject.layer = (int)team;
                return unit;
            }
        
    }
}