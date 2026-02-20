using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    public AudioSource audioSource;
    public AudioClip scene1Music;
    public AudioClip scene2Music;
    public AudioClip scene3Music;
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
            
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
                audioSource.loop = true;
            }

            SceneManager.sceneLoaded += OnSceneLoaded;
            
            // Play music for the current scene (important when starting from MainMenu)
            PlayMusicForScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            SceneManager.sceneLoaded -= OnSceneLoaded; // Unsubscribe before destroying
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (instance != this) return; // Only the persistent instance should handle this
        PlayMusicForScene(scene.name);
    }

    private void PlayMusicForScene(string sceneName)
    {
        // Ensure audioSource exists
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
                audioSource.loop = true;
            }
        }

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
            case "Scene3":
                clipToPlay = scene3Music;
                break;
            case "End":
                clipToPlay = endMusic;
                break;
            default:
                clipToPlay = menuMusic; // fallback
                break;
        }

        if (clipToPlay != null && audioSource.clip != clipToPlay)
        {
            audioSource.clip = clipToPlay;
            audioSource.Play();
        }
        else if (clipToPlay == null)
        {
            audioSource.Stop();
        }
    }
}
