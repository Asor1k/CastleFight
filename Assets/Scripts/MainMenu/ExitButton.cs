using UnityEngine;
using UnityEngine.UI;

namespace CastleFight
{
    public class ExitButton : MonoBehaviour
    {
        [SerializeField] private Button btn;

        private void Awake()
        {
            btn.onClick.AddListener(OnButtonClick);
        }

        private void OnDestroy()
        {
            btn.onClick.RemoveAllListeners();
        }

        private void OnButtonClick()
        {
            Application.Quit();
        }
    }
}