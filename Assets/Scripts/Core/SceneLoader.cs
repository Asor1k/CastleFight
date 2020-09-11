using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private Image progressImage;
    [SerializeField] private Text progressText;

    public void Start()
    {
        StartCoroutine(LoadAsynchronicaly(1));
    }

    public void LoadScene(int index)
    {
        StartCoroutine(LoadAsynchronicaly(index));
    }

    private IEnumerator LoadAsynchronicaly(int index)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(index);

        while (!operation.isDone)
        {
            progressText.text = Mathf.Round(operation.progress * 100) + "%";
            progressImage.fillAmount = operation.progress;
            yield return null;
        }
    }

}
