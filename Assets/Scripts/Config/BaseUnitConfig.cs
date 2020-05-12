using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CastleFight.Config
{
    public abstract class BaseUnitConfig : ScriptableObject
    {
        [SerializeField]
        protected Unit _prefab;
        [SerializeField]
        protected Sprite _icon;

        public abstract Unit Create();
    }
}