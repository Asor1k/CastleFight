using CastleFight.Core.EventsBus.Events;
using System.Collections.Generic;
using CastleFight.Core.EventsBus;
using CastleFight.Config;
using CastleFight.Core;
using UnityEngine;
using System.Linq;

namespace CastleFight
{
    public class PlayerProgress : MonoBehaviour
    {
        [SerializeField] private string playerProgressFileName;
        [SerializeField] private int ratingDelta;
        [SerializeField] private TalantsGenerator generator;
        public PlayerData Data => data;
        public int RatingDelta => ratingDelta;
        private PlayerData data;
       

        public void Awake()
        {
            ManagerHolder.I.AddManager(this);
            if (SaveManager.FileExists(playerProgressFileName))
            {
                data = SaveManager.Load<PlayerData>(playerProgressFileName);
                if (!data.isNotFirst)
                {
                    CreateNewData();
                }
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
            data.isNotFirst = true;
            generator.ResetConfigs();
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
        
        public void Save()
        {
            SaveManager.Save(playerProgressFileName, data);
        }

        public void SetWeigths(List<int> weights)
        {
            data.Weights = weights;
            Save();
        }
        public void SetTalants(List<int> talants)
        {
            data.TalantLevels = talants;
            Save();
        }
        private void OnGameEnd(GameEndEvent gameEndEvent)
        {
            if(gameEndEvent.won)
            {
                data.Rating += ratingDelta;
            }
            else
            {
                data.Rating -= data.Rating >= ratingDelta ? ratingDelta : data.Rating;
            }
            Save();
        }

    }
}