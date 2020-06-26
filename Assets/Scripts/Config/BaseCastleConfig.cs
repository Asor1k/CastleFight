using UnityEngine;
using CastleFight.Config;

namespace CastleFight
{
    public abstract class BaseCastleConfig : ScriptableObject
    {
        public float GoldDelay => goldDelay;
        public int GoldIncome => goldIncome;
        public int MaxHp => maxHp;
        
        [SerializeField] private Castle prefab;
        [SerializeField] protected float goldDelay;
        [SerializeField] protected int goldIncome;
        [SerializeField] protected int maxHp;

        public Castle Create()
        {
            return Instantiate(prefab);
        }


    }
}