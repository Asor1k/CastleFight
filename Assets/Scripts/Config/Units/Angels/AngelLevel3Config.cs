using System.Collections;
using System.Collections.Generic;
using CastleFight.Core;
using UnityEngine;

namespace CastleFight.Config
{
    [CreateAssetMenu(fileName = "AngelLevel3", menuName = "Units/AngelLevel3", order = 0)]
    public class AngelLevel3Config : BaseUnitConfig
    {
        public override Unit Create(Team team)
        {
            var unit = Pool.I.Get<AngelLevel3>();

            if(unit == null)
            {
                unit = Instantiate(prefab) as AngelLevel3;
                unit.Init(this, team);
                unit.gameObject.SetActive(true);
            }

            unit.gameObject.layer = (int)team;
            return unit;
        }
    }
}