using UnityEngine;

public class CoinController : MonoBehaviour
{
    public AudioClip coinSound;
    private bool collected = false;
    private static System.Collections.Generic.HashSet<GameObject> collectedCoins = new System.Collections.Generic.HashSet<GameObject>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger entered by: " + collision.gameObject.name + " with tag: " + collision.tag);
        
        if(!collision.CompareTag("Player")) return;
        if(collected) return; // Prevent double collection
        
        // Check if this GameObject has already been collected (prevents duplicate CoinControllers from counting twice)
        if (collectedCoins.Contains(gameObject))
        {
            Debug.Log("Coin GameObject already collected, skipping");
            return;
        }
        
        collected = true;
        collectedCoins.Add(gameObject);
        Debug.Log("Coin collected: " + gameObject.name);
        gameObject.SetActive(false); // Deactivate FIRST to prevent any re-entry
        
        // Find ScoreManager if it's null
        if (ScoreManager.scoreManager == null)
        {
            ScoreManager.scoreManager = FindFirstObjectByType<ScoreManager>();
        }
        
        if (ScoreManager.scoreManager != null)
        {
            ScoreManager.scoreManager.RaiseScore(1);
        }
        else
        {
            Debug.LogError("ScoreManager.scoreManager is NULL and couldn't be found!");
        }
        
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlaySound(coinSound);
        }
        else
        {
            // Try to find AudioManager or AudioSource directly
            AudioManager.instance = FindFirstObjectByType<AudioManager>();
            if (AudioManager.instance != null)
            {
                AudioManager.instance.PlaySound(coinSound);
            }
            else
            {
                // Fallback: find any AudioSource and use it
                AudioSource[] audioSources = FindObjectsByType<AudioSource>(FindObjectsSortMode.None);
                if (audioSources.Length > 0)
                {
                    audioSources[0].PlayOneShot(coinSound);
                }
            }
        }
        
    }
}
