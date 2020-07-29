using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using CastleFight.Core.EventsBus;


namespace CastleFight
{
    public class EntitiesNumber : MonoBehaviour
    {
        [SerializeField] private Text entitiesText;
        [SerializeField] private Text fpsText;
        [SerializeField] private int target;
        private int units = 0;
        private void Start()
        {
            EventBusController.I.Bus.Subscribe<UnitSpawnedEvent>(OnUnitSpawned);
            EventBusController.I.Bus.Subscribe<UnitDiedEvent>(OnUnitDied);
            UpdateUnitsText();
            StartCoroutine(UpdateFps());
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60;
        }

        public void Update()
        {
        }
        private IEnumerator UpdateFps()
        {
            yield return new WaitForSeconds(0.2f);
            fpsText.text = "FPS: " + Mathf.RoundToInt(1 / Time.deltaTime);
            StartCoroutine(UpdateFps());
        }
        

        private void OnUnitDied(UnitDiedEvent unitDiedEvent)
        {
            units--;
            UpdateUnitsText();
        }
        private void OnUnitSpawned(UnitSpawnedEvent unitSpawnedEvent)
        {
            units++;
            UpdateUnitsText();
        }

        private void UpdateUnitsText()
        {
            entitiesText.text = "Units: " + units.ToString();
        }
        private void OnDestroy()
        {
            EventBusController.I.Bus.Unsubscribe<UnitSpawnedEvent>(OnUnitSpawned);
            EventBusController.I.Bus.Unsubscribe<UnitDiedEvent>(OnUnitDied);
        }
    }
}