using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CastleFight
{
    public interface IDamageable
    {
        bool Alive { get; }
        TargetType Type { get; }
        Transform Transform{get;}
        void TakeDamage(int damage);
    }

    public enum TargetType
    {
        GroundUnit,
        Building,
        Castle
    }
}