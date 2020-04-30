using UnityEngine;

namespace CastleFight.Core
{
    public class GameController : MonoBehaviour
    {
        private void Awake()
        {
            ManagerHolder.I.AddManager(this);
        }
    }
}

