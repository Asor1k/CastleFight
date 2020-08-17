using CastleFight.Config;
using System.Collections.Generic;
using CastleFight.Core.EventsBus;
using UnityEngine;

namespace CastleFight.MainMenu
{
    public class RaceMenu : MonoBehaviour
    {
        [SerializeField] private RaceSet raceSet;
        [SerializeField] private RectTransform holder;
        [SerializeField] private RaceButton btnPrefab;

        private RaceChosenEvent RaceChosenEvent => raceChosenEvent ?? (raceChosenEvent = new RaceChosenEvent());
        
        private RaceChosenEvent raceChosenEvent;

        private List<RaceButton> raceButtons = new List<RaceButton>();

        private void Start()
        {
            PrepareRaces();
        }

        private void PrepareRaces()
        {
            foreach (var config in raceSet.RaceConfigs)
            {
                var btn = InstantiateBtn();
                btn.Init(config);
                btn.Click += OnRaceChosen;
                raceButtons.Add(btn);
            }
        }

        private void OnRaceChosen(RaceConfig config)
        {
            foreach(RaceButton btn in raceButtons)
            {
                btn.SetDisabled();
            }
            RaceChosenEvent.SetUserConfig(config);
            EventBusController.I.Bus.Publish<RaceChosenEvent>(RaceChosenEvent);
        }

        private RaceButton InstantiateBtn()
        {
            return Instantiate(btnPrefab, holder);
        }
    }
}