using CastleFight.Core;
using UnityEngine;

namespace CastleFight
{
    public class UserController : MonoBehaviour
    {
        [SerializeField] private UserAbility[] abilities;
        
        public void StartGame()
        {
            UnlockAbilities();
            // TODO: show UI
        }

        public void StopGame()
        {
            LockAbilities();
            // TODO: hide UI
        }

        private void UnlockAbilities()
        {
            foreach (var ability in abilities)
            {
                ability.Unlock();
            }
        }

        private void LockAbilities()
        {
            foreach (var ability in abilities)
            {
                ability.Lock();
            }
        }
    }
}