using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class GoldAnim : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    public void Init(int gold)
    {
        text.text = gold.ToString();
        Vector3 cameraPosition = Camera.main.transform.position;
        Vector3 animPosition = transform.position;

        transform.LookAt(new Vector3(cameraPosition.x, cameraPosition.y, animPosition.z));
    }
}
