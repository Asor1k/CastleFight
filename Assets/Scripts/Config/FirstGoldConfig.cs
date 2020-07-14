using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CastleFight {

    [CreateAssetMenu(fileName = "FirstGoldConfig", menuName = "Settings/FirstGold", order = 0)]
    public class FirstGoldConfig : ScriptableObject
    {
        public int UserGold => userGold;
        public int BotGold => botGold;

        [SerializeField] private int userGold;
        [SerializeField] private int botGold;
    }
}