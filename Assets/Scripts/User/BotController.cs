using UnityEngine;

namespace CastleFight
{
    public class BotController : MonoBehaviour
    {
        [SerializeField] private CastlesPosProvider castlesPosProvider;

        public void Init(RaceConfig config)
        {
            CreateCastle(config.CastleConfig);
        }

        private void CreateCastle(CastleConfig castleConfig)
        {
            var castleHolder = castlesPosProvider.GetCastlePos(this);
            var castlePos = castleHolder.position;
            castlePos = new Vector3(castlePos.x, castlePos.y + 2.5f, castlePos.z); // TODO: remove magic number
            var castle = castleConfig.Create();
            castle.transform.position = castlePos;
        }

        public void StartGame()
        {
        }

        public void StopGame()
        {
        }
    }
}