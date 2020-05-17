using System.Collections.Generic;
using CastleFight.Core.EventsBus;
using CastleFight.Core.EventsBus.Events;

namespace CastleFight.Core
{
    public class BuildingManager
    {
        public static BuildingManager I => instance ?? (instance = new BuildingManager());

        private static BuildingManager instance;

        private List<BuildingBehavior> buildings = new List<BuildingBehavior>();

        /// <summary>
        /// Make sure this method called only once during run time. 
        /// </summary>
        public void Init()
        {
            EventBusController.I.Bus.Subscribe<BuildingPlacedEvent>(OnBuildingPlaced);
            EventBusController.I.Bus.Subscribe<ExitToMainMenuEvent>(OnExitToMainMenu);
            EventBusController.I.Bus.Subscribe<GameEndEvent>(OnGameEnded);
        }

        private void OnGameEnded(GameEndEvent gameEndEvent)
        {
            Clear();
        }

        private void OnExitToMainMenu(ExitToMainMenuEvent mainMenuEvent)
        {
            Clear();
        }

        private void OnBuildingPlaced(BuildingPlacedEvent placedEvent)
        {
            buildings.Add(placedEvent.behavior);
        }

        private void Clear()
        {
            foreach (var building in buildings)
            {
                building.Destroy();
            }

            buildings.Clear();
        }
    }
}