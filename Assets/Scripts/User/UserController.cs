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
        [SerializeField] private CastlesPosProvider castlesPosProvider;

        private GameUIBehavior gameUI;

        public void Init(RaceConfig config)
        {
            if (gameUI == null)
            {
                gameUI = Instantiate(gameUILayoutPrefab, gameUIHolder);
            }

            gameUI.Hide();
            gameUI.Init(config.BuildingSet);

            CreateCastle(config.CastleConfig);
        }

        private void CreateCastle(CastleConfig castleConfig)
        {
            var castleHolder = castlesPosProvider.GetCastlePos(this);
            var castlePos = castleHolder.position;
            castlePos = new Vector3(castlePos.x, castlePos.y + 2.5f, castlePos.z); // TODO: remove magic number
            var castle = castleConfig.Create();
            castle.transform.position = castlePos;
            castle.gameObject.layer = (int)Team.Team1;
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