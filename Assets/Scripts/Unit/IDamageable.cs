using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CastleFight
{
    public interface IDamageable
    {
        bool Alive{get;}
        Transform Transform{get;}
        void TakeDamage(int damage);
    }
}