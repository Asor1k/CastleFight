using CastleFight.Config;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CastleFight
{
    public class Building : MonoBehaviour, IBuildingCreateHandler
    {
        [SerializeField]
        private GameObject _spawnPoint;
        private BaseUnitConfig _unit;
        private float _spawnDelay;

        private Coroutine _spawnCoroutine = null;

        private void Start()
        {
            _spawnCoroutine = StartCoroutine(SpawnCoroutine());    
        }

        public void Init(BaseUnitConfig unit, float spawnDelay)
        {
            _unit = unit;
        }

        private void SpawnUnit() 
        {
            var unit = _unit.Create();
            unit.transform.position = _spawnPoint.transform.position;
        }

        private IEnumerator SpawnCoroutine()
        {
            yield return new WaitForSeconds(_spawnDelay);
            SpawnUnit();
        }

        void IBuildingCreateHandler.MoveTo(Vector3 position)
        {
            transform.position = position;
        }

        void IBuildingCreateHandler.Place()
        {
            throw new System.NotImplementedException();
        }

        bool IBuildingCreateHandler.CanBePlaced()
        {
            throw new System.NotImplementedException();
        }
    }
}
