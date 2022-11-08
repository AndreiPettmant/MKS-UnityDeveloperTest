using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneController : MonoBehaviour
{
    [Header("Load scene name")]
    [SerializeField] private string sceneName;
    [Header("Load delay")]
    [SerializeField] private float loadDelay;
    public void ChangeScene()
    {
        Invoke("LoadScene", loadDelay);
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
