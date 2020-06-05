using CastleFight.Config;
using UnityEngine;

namespace CastleFight
{
    public abstract class BaseBuildingConfig : ScriptableObject
    {
        public Sprite Icon => icon;
        public float Delay => delay;
        public BaseUnitConfig Unit => unit;
        public float Cost => cost;

        [SerializeField]
        protected Building prefab;
        [SerializeField]
        protected BaseUnitConfig unit;
        [SerializeField]
        protected float delay;
        [SerializeField]
        protected Sprite icon;
        [SerializeField]
        protected float cost;
        
        public virtual Building Create()
        {
            
            var building = Instantiate(prefab); // TODO: get from pool
            building.Init(this);

            return building;
        }
    }
}