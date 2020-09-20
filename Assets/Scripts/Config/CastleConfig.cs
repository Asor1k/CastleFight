using UnityEngine;

namespace CastleFight
{
    [CreateAssetMenu(fileName = "Castle", menuName = "Buildings/Castle", order = 0)]
    public class CastleConfig : BaseCastleConfig
    {
        public float AttackRange { get { return attackRange; } }
        public float AttackDelay { get { return attackDelay; } }
        public float Damage { get { return damage; } }
        public Material Material { get { return material; } }

        [SerializeField]
        private float attackRange;
        [SerializeField]
        private float attackDelay;
        [SerializeField]
        private float damage;
        [SerializeField]
        private Material material;
    }
}