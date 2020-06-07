using System.Collections;
using CastleFight.Core.EventsBus;
using CastleFight.Core.EventsBus.Events;
using UnityEngine;
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
        private UserController user;

        public void Start()
        {
            user = GetComponent<UserController>();
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

        public bool IsEnogh(BuildingBehavior buildingBehavior)
        {
            bool canPlace = goldAmount >= buildingBehavior.building.Config.Cost;
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
            Text text = Instantiate(notEnoghGoldText, user.gameUIHolder);
            StartCoroutine(DestroyText(text));
            //Debug.Log("Not Enough Gold!!");
        }
    }
}
