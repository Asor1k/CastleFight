using System;
using UnityEngine;

namespace CastleFight.Config
{
    public abstract class BaseUnitConfig : ScriptableObject
    {
        [SerializeField]
        protected Unit prefab;
        [SerializeField]
        protected Sprite icon;

        public abstract Unit Create();
    }
}