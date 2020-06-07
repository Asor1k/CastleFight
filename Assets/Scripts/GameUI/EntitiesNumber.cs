using UnityEngine;
using UnityEngine.UI;
using CastleFight.Core.EventsBus;
namespace CastleFight
{
    public class EntitiesNumber : MonoBehaviour
    {
        Text entitiesText;
        int number = 0;
        private void Start()
        {
            EventBusController.I.Bus.Subscribe<UnitSpawnedEvent>(OnUnitSpawned);
            entitiesText = GetComponent<Text>();
        }

        private void OnUnitSpawned(UnitSpawnedEvent unitSpawnedEvent)
        {
            number++;
            entitiesText.text = number.ToString();
        }

    }
}