using CastleFight.Config;
using System.Collections;
using System.Collections.Generic;
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

        public virtual Building Create()
        {
            var building = Instantiate(prefab);
            building.Init(unit, delay);

            return building;
        }
    }
}