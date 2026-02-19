using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void NextLevel()
    {
        StartCoroutine(LoadLevelAsync(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadLevelAsync(sceneName));
    }

    private IEnumerator LoadLevelAsync(int buildIndex)
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(buildIndex);
        op.allowSceneActivation = false; // Wait to activate

        // Optional: fade-out or wait
        yield return new WaitForSecondsRealtime(0.1f);

        op.allowSceneActivation = true; // Now activate scene
    }

    private IEnumerator LoadLevelAsync(string sceneName)
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
        op.allowSceneActivation = false;

        yield return new WaitForSecondsRealtime(0.1f);

        op.allowSceneActivation = true;
    }
}
