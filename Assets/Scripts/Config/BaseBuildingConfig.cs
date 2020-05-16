using CastleFight.Config;
using UnityEngine;

namespace CastleFight
{
    public abstract class BaseBuildingConfig : ScriptableObject
    {
        [SerializeField]
        protected Building prefab;
        [SerializeField]
        protected BaseUnitConfig unit;
        [SerializeField]
        protected float delay;
        [SerializeField]
        protected Sprite icon;

        public Sprite Icon => icon;
        
        public virtual Building Create()
        {
            var building = Instantiate(prefab); // TODO: get from pool
            building.Init(unit, delay);

            return building;
        }
    }
}