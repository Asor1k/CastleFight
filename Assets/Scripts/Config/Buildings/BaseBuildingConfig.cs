using CastleFight.Config;
using UnityEngine;

namespace CastleFight
{
    public abstract class BaseBuildingConfig : ScriptableObject
    {
        public Sprite Icon => icon;
        public float Delay => delay;
        public BaseUnitConfig Unit => unit;
        public int Cost => cost;
        public float GoldDelay => goldDelay;
        public int GoldIncome => goldIncome;
        public string Name => buildingName;

        [SerializeField]
        protected Building prefab;
        [SerializeField]
        protected BaseUnitConfig unit;
        [SerializeField]
        protected float delay;
        [SerializeField]
        protected Sprite icon;
        [SerializeField]
        protected int cost;
        [SerializeField]
        protected float goldDelay;
        [SerializeField]
        protected int goldIncome;
        [SerializeField]
        protected string buildingName;
        
        public virtual Building Create()
        {
            
            var building = Instantiate(prefab); // TODO: get from pool
            building.Init(this);

            return building;
        }
    }
}