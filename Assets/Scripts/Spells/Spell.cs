using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CastleFight.Spells
{
    public abstract class Spell : MonoBehaviour
    {
        public float Cooldown { get { return cooldown; } }
        public float Radius { get { return radius; } }

        [SerializeField]
        protected float cooldown;
        [SerializeField]
        protected float radius;

        public abstract void Execute(Vector3 targetPoint, Team affectTeam);
    }
}