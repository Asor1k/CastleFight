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
        protected Unit _unit;
        [SerializeField]
        protected Sprite _icon;

        public abstract Building Create();
    }
}