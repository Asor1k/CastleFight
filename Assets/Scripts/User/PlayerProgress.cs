using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CastleFight
{
    public class PlayerProgress : MonoBehaviour
    {
        public PlayerData Data => data;
        private PlayerData data;
        [SerializeField] private string playerProgressFileName;
        public void Start()
        {
            if (SaveManager.FileExists(playerProgressFileName))
            {
                data = SaveManager.Load<PlayerData>(playerProgressFileName);
            }
            else
            {
                data = new PlayerData();
                SaveManager.Save(playerProgressFileName, data);
            }
        }
        public void OnApplicationQuit()
        {
            SaveManager.Save(playerProgressFileName, data);
        }
    }
}