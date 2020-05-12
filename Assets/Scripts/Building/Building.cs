using CastleFight.Config;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CastleFight
{
    public class Building : MonoBehaviour
    {
        [SerializeField]
        private GameObject spawnPoint;
        private BaseUnitConfig unitConfig;
        private float spawnDelay;
   
        private Coroutine _spawnCoroutine = null;

        public void Init(BaseUnitConfig unitConfig, float spawnDelay)
        {
            this.unitConfig = unitConfig;
        }

        private void SpawnUnit() 
        {
            var unit = unitConfig.Create();
            unit.transform.position = spawnPoint.transform.position;
        }

        private IEnumerator SpawnCoroutine()
        {
            yield return new WaitForSeconds(spawnDelay);
            SpawnUnit();
        }
    }
}