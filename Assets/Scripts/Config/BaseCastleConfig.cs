using UnityEngine;
using CastleFight.Config;

namespace CastleFight
{
    public abstract class BaseCastleConfig : ScriptableObject
    {
        [SerializeField] private Castle prefab;
        public float GoldDelay => goldDelay;
        public int GoldIncome => goldIncome;

        [SerializeField]
        protected float goldDelay;
        [SerializeField]
        protected int goldIncome;


        public Castle Create()
        {
            return Instantiate(prefab);
        }


    }
}