using UnityEngine;

namespace CastleFight
{
    public abstract class BaseCastleConfig : ScriptableObject
    {
        [SerializeField] private Building prefab;

        public Building Create()
        {
            return Instantiate(prefab);
        }
    }
}