using CastleFight.Core.EventsBus.Events;
using CastleFight.Core.EventsBus;
using CastleFight.Core;
using UnityEngine;

namespace CastleFight
{
    public class PlayerProgress : MonoBehaviour
    {
        public PlayerData Data => data;
        public int RatingDelta => ratingDelta;
        private PlayerData data;
        [SerializeField] private string playerProgressFileName;
        [SerializeField] private int ratingDelta;
       
        public void Awake()
        {
            ManagerHolder.I.AddManager(this);
            if (SaveManager.FileExists(playerProgressFileName))
            {
                data = SaveManager.Load<PlayerData>(playerProgressFileName);
            }
            else
            {
                CreateNewData();
            }
            if (data == null)
            {
                CreateNewData();
            }
            EventBusController.I.Bus.Subscribe<GameEndEvent>(OnGameEnd);
        }

        private void CreateNewData()
        {
            data = new PlayerData();
            SaveManager.Save(playerProgressFileName, data);
        }
        public void OnApplicationQuit()
        {
            Save();
        }
        public void OnDestroy()
        {
            Save();
        }
        private void Save()
        {
            SaveManager.Save(playerProgressFileName, data);
        }
        public void OnGameEnd(GameEndEvent gameEndEvent)
        {
            if(gameEndEvent.winnerTeam == Team.Team2)
            {
                data.Rating += ratingDelta;
            }
            else
            {
                data.Rating -= data.Rating >= ratingDelta ? ratingDelta : data.Rating;
            }
        }

    }
}