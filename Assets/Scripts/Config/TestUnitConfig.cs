using CastleFight.Config;
using UnityEngine;
using CastleFight.Core;

namespace CastleFight
{
    [CreateAssetMenu(fileName = "TestUnit", menuName = "Units/TestUnit", order = 1)]
    public class TestUnitConfig : BaseUnitConfig
    {
        public override Unit Create()
        {
            Unit unit = Pool.I.Get<TestUnit>();

            if(unit == null)
                unit = Instantiate(prefab);

            return unit;
        }
    }
}