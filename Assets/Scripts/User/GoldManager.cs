using System.Collections;
using CastleFight.Core;
using CastleFight.Core.EventsBus;
using CastleFight.Core.EventsBus.Events;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;


namespace CastleFight
{
    public class GoldManager : MonoBehaviour
    {   
        [SerializeField]
        private Text goldText;
        [SerializeField]
        private int goldAmount = 0;
        [SerializeField]
        private Text notEnoghGoldText;
        [SerializeField]
        private UserController userController;

        public void Start()
        {
            ManagerHolder.I.AddManager(this);
            EventBusController.I.Bus.Subscribe<GameSetReadyEvent>(OnGameStart);
        }

        private void OnGameStart(GameSetReadyEvent gameSetReadyEvent)
        {
            goldText = FindObjectOfType<GoldText>().GetComponent<Text>();
            goldText.text = goldAmount.ToString();
        }
        

        private void OnDestroy()
        {
            EventBusController.I.Bus.Unsubscribe<GameSetReadyEvent>(OnGameStart);
        }

        public bool IsEnough(BuildingBehavior buildingBehavior)
        {
            bool canPlace = goldAmount >= buildingBehavior.Building.Config.Cost;
            if (!canPlace)
            { 
                NotEnoughGold();
            }
            return canPlace;
        }
        public void MakeGoldChange(int gold)
        {
            goldAmount += gold;
            goldText.text = goldAmount.ToString();
        }

        private IEnumerator DestroyText(Text text)
        {
            yield return new WaitForSeconds(1);
            Destroy(text.gameObject);
        }
        private void NotEnoughGold()
        {
            Text text = Instantiate(notEnoghGoldText, userController.gameUIHolder);
            StartCoroutine(DestroyText(text));
            //Debug.Log("Not Enough Gold!!");
        }
    }
}
