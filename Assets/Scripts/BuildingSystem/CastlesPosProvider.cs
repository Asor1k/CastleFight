using UnityEngine;

namespace CastleFight
{
    public class CastlesPosProvider : MonoBehaviour
    {
        [SerializeField] private Transform userCastlePos;
        [SerializeField] private Transform botCastlePos;

        public Transform GetCastlePos(UserController userController)
        {
            return userCastlePos;
        }

        public Transform GetCastlePos(BotController botController)
        {
            return botCastlePos;
        }
    }
}