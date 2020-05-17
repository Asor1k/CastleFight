using CastleFight.Config;
using System.Collections;
using UnityEngine;

namespace CastleFight
{
    public class Building : MonoBehaviour
    {
        [SerializeField]
        private Transform spawnPoint;
        [SerializeField]
        private BuildingBehavior behavior;

        public BuildingBehavior Behavior => behavior;
        
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
            unit.transform.position = spawnPoint.position;
        }

        private IEnumerator SpawnCoroutine()
        {
            yield return new WaitForSeconds(spawnDelay);
            SpawnUnit();
        }
    }
}