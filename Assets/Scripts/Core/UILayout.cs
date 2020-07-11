using UnityEngine;

namespace CastleFight.Core
{
    public class UILayout : MonoBehaviour
    {
        [SerializeField] protected Canvas canvas;

        public virtual void Show()
        {
            SetCanvasEnabled(true);
        }

        public virtual void Hide()
        {
            SetCanvasEnabled(false);
        }

        protected virtual void SetCanvasEnabled(bool enabled)
        {
            canvas.enabled = enabled;
        }
    }
}