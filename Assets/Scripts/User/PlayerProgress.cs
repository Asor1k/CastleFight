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
            EventBusController.I.Bus.Subscribe<GameEndEvent>(OnGameEnd);
            ManagerHolder.I.AddManager(this);
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