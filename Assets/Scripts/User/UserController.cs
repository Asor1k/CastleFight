using CastleFight.Core;
using CastleFight.GameUI;
using UnityEngine;

namespace CastleFight
{
    public class UserController : MonoBehaviour
    {
        [SerializeField] private UserAbility[] abilities;
        [SerializeField] private GameUIBehavior gameUILayoutPrefab;
        [SerializeField] private RectTransform gameUIHolder;

        private GameUIBehavior gameUI;

        public void Init(RaceConfig config)
        {
            if (gameUI == null)
            {
                gameUI = Instantiate(gameUILayoutPrefab, gameUIHolder);
            }

            gameUI.Hide();
            gameUI.Init(config.BuildingSet);
        }

        public void StartGame()
        {
            UnlockAbilities();
            gameUI.Show();
        }

        public void StopGame()
        {
            LockAbilities();
            gameUI.Hide();
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