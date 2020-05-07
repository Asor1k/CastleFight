using CastleFight.Core;
using UnityEngine;
using UnityEngine.UI;

namespace CastleFight
{
    public class MainMenuLayout : UILayout
    {
        [SerializeField] private Button playButton;

        private void OnEnable()
        {
            playButton.onClick.AddListener(PrepareGameSet);
        }

        private void OnDisable()
        {
            playButton.onClick.RemoveAllListeners();
        }
        
        private void PrepareGameSet()
        {
            GameObject.Find("CoreHolder").GetComponent<GameManager>().StartGame();
        }
    }
}