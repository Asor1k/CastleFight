using CastleFight.Core;
using UnityEngine.UI;
using UnityEngine;
namespace CastleFight
{
    public class RatingUI : MonoBehaviour
    {
        [SerializeField] private Text ratingText;

        public void Start()
        {
            PlayerProgress playerProgress = ManagerHolder.I.GetManager<PlayerProgress>();
            if (playerProgress == null) return;
            ratingText.text = "Raiting:" + playerProgress.Data.Rating;
        }
    }
}