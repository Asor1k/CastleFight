using CastleFight.Core;
using UnityEngine;

namespace CastleFight.Config
{
    [CreateAssetMenu(fileName = "Circle", menuName = "Highlight/Circle", order = 0)]
    public class CircleConfig : ScriptableObject
    {
        [SerializeField] private UnitHighlight prefab;

        public UnitHighlight Create(Transform tr)
        {
            var circle = Pool.I.Get<UnitHighlight>();
            if (circle == null)
            {
                circle = Instantiate(prefab) as UnitHighlight;
                
            }
            circle.Init(tr);
            return circle;
        }
    }
}
