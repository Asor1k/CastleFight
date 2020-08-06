using System.Collections;
using System.Collections.Generic;
using CastleFight.Core.EventsBus;
using CastleFight.Core.EventsBus.Events;
using UnityEngine;
using UnityEngine.AI;

namespace CastleFight
{
    public class BuildingBehavior : MonoBehaviour
    {
        public Team Team => team;
        public Building Building => building;
        public float OffsetY  => offsetY;
        public bool IsPlaced => isPlaced;

        [SerializeField] private Collider col;
        [SerializeField] private NavMeshObstacle obstacle;
        [SerializeField] private Building building;
        [SerializeField] private float offsetY; //TODO: Move to config
        [SerializeField] private UserController user;
        [SerializeField] private MeshRenderer rend;

        private List<Collider> collisions = new List<Collider>();
        private Team team;
        private bool isPlaced = false;
        
        public void Place(Team team)
        {
            obstacle.enabled = true;
            col.enabled = true;
            this.team = team;
            gameObject.layer = (int)team;
            col.isTrigger = false;
            isPlaced = true;

            building.Build();
            if(team == Team.Team2)
            {
                transform.eulerAngles = new Vector3(0, 180, 0); 
            }
            EventBusController.I.Bus.Publish(new BuildingPlacedEvent(this));
            
        }
        
        /*private void OnMouseEnter()
        {
            if (!isPlaced) return;
            rend.material.shader = Shader.Find("Outlined/Custom");
        }

        private void OnMouseExit()
        {
            if (!isPlaced) return;
            rend.material.shader = Shader.Find("Standard");
        }
        */
        /*IEnumerator StartMoneyGain()
        {
            yield return new WaitForSeconds(building.Config.Levels[building.Lvl - 1].GoldDelay);
            StartCoroutine(StartMoneyGain());
            building.GoldManager.MakeGoldChange(building.Config.Levels[building.Lvl - 1].GoldIncome);
        }*/
        
        public void MoveTo(Vector3 position)
        {
            transform.position = position;
        }

        public bool CanBePlaced()
        {
            if (rend == null)
            {
                Debug.LogError("There no renderer reference on this building!");
                return true;
            }
            if (collisions.Count == 0)  
            {
                rend.material.color = Color.white; //Make color changes using shaders
                return true;
            }
            rend.material.color = Color.red;
            return false;
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.CompareTag("Map")) return;
            collisions.Add(collider);
        }

        private void OnTriggerExit(Collider collider)
        {
            collisions.Remove(collider);
        }

        public void Destroy()
        {
            if (gameObject == null) return;
            Destroy(gameObject); // TODO: move to pool
        }
    }
}