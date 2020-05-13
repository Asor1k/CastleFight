using CastleFight.Config;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CastleFight
{
    [CreateAssetMenu(fileName = "TestUnit", menuName = "Units/TestUnit", order = 1)]
    public class TestUnitConfig : BaseUnitConfig
    {
        public override Unit Create()
        {
            return Instantiate(prefab);
        }
    }
}