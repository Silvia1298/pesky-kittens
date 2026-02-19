using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    public AudioSource audioSource;
    public AudioClip scene1Music;
    public AudioClip scene2Music;
    public AudioClip menuMusic;
    public AudioClip endMusic;
    // Add more clips for additional scenes

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            if (audioSource == null)
                audioSource = GetComponent<AudioSource>();

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayMusicForScene(scene.name);
    }

    private void PlayMusicForScene(string sceneName)
    {
        AudioClip clipToPlay = null;

        switch (sceneName)
        {
            case "MainMenu":
                clipToPlay = menuMusic;
                break;
            case "Scene1":
                clipToPlay = scene1Music;
                break;
            case "Scene2":
                clipToPlay = scene2Music;
                break;
            case "End":
                clipToPlay = endMusic;
                break;
            default:
                clipToPlay = scene1Music; // fallback
                break;
        }

        if (clipToPlay != null && audioSource.clip != clipToPlay)
        {
            audioSource.clip = clipToPlay;
            audioSource.Play();
        }
    }
}
