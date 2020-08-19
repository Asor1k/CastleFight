using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CastleFight.Spells
{
    public abstract class Spell : ScriptableObject
    {
        public float Cooldown { get { return cooldown; } }
        [SerializeField]
        protected float cooldown;

        public abstract void Execute(Vector3 targetPoint, Team affectTeam);
    }
}