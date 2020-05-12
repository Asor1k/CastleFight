using CastleFight.Config;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CastleFight
{
    public abstract class BaseBuildingConfig : ScriptableObject
    {
        [SerializeField]
        protected Building _prefab;
        [SerializeField]
        protected BaseUnitConfig _unit;
        [SerializeField]
        protected float _delay;
        [SerializeField]
        protected Sprite _icon;

        public virtual Building Create()
        {
            var building = Instantiate(_prefab);
            building.Init(_unit, _delay);

            return building;
        }
    }
}