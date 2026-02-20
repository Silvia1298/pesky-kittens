using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    private AudioSource audioSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            audioSource = GetComponent<AudioSource>();
            
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
            
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(AudioClip clip)
    {
        if (clip == null)
        {
            return;
        }

        // Ensure audioSource exists
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }
        
        if (audioSource != null)
        {
            audioSource.PlayOneShot(clip, 1f);
        }
    }
}
