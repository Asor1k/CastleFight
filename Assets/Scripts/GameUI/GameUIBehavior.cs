using System.Collections.Generic;
using CastleFight.Core;
using CastleFight.Core.EventsBus;
using CastleFight.Core.EventsBus.Events;
using UnityEngine;

namespace CastleFight.GameUI
{
    public class GameUIBehavior : MonoBehaviour
    {
        [SerializeField] private UILayout layout;
        [SerializeField] private RectTransform buldingsHodler;
        [SerializeField] private BuildingButton buildingBtnPrefab;

       private Dictionary<BuildingButton, BaseBuildingConfig> buildingBtnsTable = new Dictionary<BuildingButton, BaseBuildingConfig>();

        public void Init(BuildingSet set)
        {
            Clear();
            foreach (var config in set.BuildingConfigs)
            {
                var btn = Instantiate(buildingBtnPrefab, buldingsHodler); // TODO: get from pool
                btn.Init(config);
                btn.Click += OnBuildingBtnClick;
                buildingBtnsTable.Add(btn, config);
                Debug.Log(buildingBtnsTable.Count);
            }
        }

        private void OnBuildingBtnClick(BuildingButton btn)
        {
            var config = buildingBtnsTable[btn];
            EventBusController.I.Bus.Publish(new BuildingChosenEvent(config));
        }

        //Begin [Asor1k]
        public void Start()
        {
            EventBusController.I.Bus.Subscribe<RestartGameEvent>(OnRestartGame);
        }

        private void OnRestartGame(RestartGameEvent gameSetReady)
        {
            Hide();
        }

        public void OnDestroy()
        {
            EventBusController.I.Bus.Unsubscribe<RestartGameEvent>(OnRestartGame);
            
        }
        //End [Asor1k]

        private void Clear()
        {
           // Debug.Log(buildingBtnsTable.Count);
            if (buildingBtnsTable != null)
            {
                foreach (var pair in buildingBtnsTable)
                {
                    pair.Key.Click -= OnBuildingBtnClick;
                   
                    pair.Key.Destroy();
                }

                buildingBtnsTable.Clear();
            }
        }
    
        public void Show()
        {
            layout.Show();
        }

        public void Hide()
        {
            layout.Hide();
        }
    }
}