using UnityEngine;

namespace CastleFight.Core
{
    public abstract class UserAbility : MonoBehaviour
    {
        public abstract void Unlock();
        public abstract void Lock();
    }
}