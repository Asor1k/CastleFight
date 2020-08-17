using System.Collections;
using System.Collections.Generic;
using CastleFight.Core.EventsBus;
using UnityEngine;
using Unity.Jobs;
using Unity.Collections;
using CastleFight.Core.EventsBus.Events;

namespace CastleFight
{
    public class UnitManager : MonoBehaviour
    {
        public static UnitManager I = null;

        private List<IDamageable> westUnits = new List<IDamageable>();
        private List<IDamageable> eastUnits = new List<IDamageable>();
        private List<IDamageable> westBuildings = new List<IDamageable>();
        private List<IDamageable> eastBuildings = new List<IDamageable>();

        private void Awake()
        {
            if (I == null)
            {
                I = this;
            }
        }

        private void Start()
        {
            EventBusController.I.Bus.Subscribe<UnitSpawnedEvent>(OnUnitSpawned);
            EventBusController.I.Bus.Subscribe<UnitDiedEvent>(OnUnitDied);
            EventBusController.I.Bus.Subscribe<BuildingPlacedEvent>(OnBuildingPlaced);
            EventBusController.I.Bus.Subscribe<BuildingDestroyedEvent>(OnBuildingDestroyed);
        }

        private void OnUnitDied(UnitDiedEvent unitDiedEvent)
        {
            var unit = unitDiedEvent.Unit;

            if (unit.Team == Team.Team1)
            {
                westUnits.Remove(unit.DamageBehaviour);
            }
            else
            {
                eastUnits.Remove(unit.DamageBehaviour);
            }
        }

        private void OnUnitSpawned(UnitSpawnedEvent unitSpawnedEvent)
        {
            var unit = unitSpawnedEvent.Unit;

            if (unit.Team == Team.Team1)
            {
                westUnits.Add(unit.DamageBehaviour);
            }
            else
            {
                eastUnits.Add(unit.DamageBehaviour);
            }
        }

        private void OnBuildingDestroyed(BuildingDestroyedEvent buildingDestroyedEvent)
        {
            var building = buildingDestroyedEvent.building;

            if (building.Behavior.Team == Team.Team1)
            {
                westBuildings.Remove(building.Damageable);
            }
            else
            {
                eastBuildings.Remove(building.Damageable);
            }
        }

        private void OnBuildingPlaced(BuildingPlacedEvent buildingPlacedEvent)
        {
            var buildingBehaviour = buildingPlacedEvent.behavior;

            if (buildingBehaviour.Team == Team.Team1)
            {
                westBuildings.Add(buildingBehaviour.Building.Damageable);
            }
            else
            {
                eastBuildings.Add(buildingBehaviour.Building.Damageable);
            }
        }

        public IDamageable GetClossestUnit(Vector3 point, Team team, bool ignoreAir)
        {
            List<IDamageable> units;

            if (team == Team.Team1)
            {
                units = eastUnits;
            }
            else
            {
                units = westUnits;
            }

            IDamageable closest = null;
            float closestDistance = 100;

            foreach (var unit in units)
            {
                if (ignoreAir
                    && unit.Type == TargetType.AirUnit) continue;

                var distance = Vector3.Distance(point, unit.Transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closest = unit;
                }
            }

            return closest;
        }

        public IDamageable GetClossestBuilding(Vector3 point, Team team)
        {
            List<IDamageable> buildings;

            if (team == Team.Team1)
            {
                buildings = eastBuildings;
            }
            else
            {
                buildings = westBuildings;
            }

            IDamageable closest = null;
            float closestDistance = 100;

            foreach (var building in buildings)
            {
                var distance
                    = Vector3.Distance(point, building.Transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closest = building;
                }
            }

            return closest;
        }
    }
}