using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI coinsCollected;
    public static ScoreManager scoreManager;
    int score = 0;
    int totalCoins = 0;

    private void Awake()
    {
        if (scoreManager == null)
        {
            scoreManager = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            // Transfer UI reference if this one has it but the persistent one doesn't
            if (coinsCollected != null && scoreManager.coinsCollected == null)
            {
                scoreManager.coinsCollected = coinsCollected;
            }
            Destroy(gameObject);
            return; // Important: exit early to prevent Start() from running
        }
    }

    private void OnDestroy()
    {
        if (scoreManager == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Reset score and recount coins for new scene
        score = 0;
        
        // Find all ScoreManagers in the scene (including the local one that will be destroyed)
        ScoreManager[] allManagers = FindObjectsByType<ScoreManager>(FindObjectsSortMode.None);
        
        // Get the UI reference from the local ScoreManager before it gets destroyed
        foreach (ScoreManager manager in allManagers)
        {
            if (manager != this && manager.coinsCollected != null)
            {
                coinsCollected = manager.coinsCollected;
                break;
            }
        }
        
        // Count unique GameObjects with CoinController to avoid counting duplicates
        CoinController[] allCoinControllers = FindObjectsByType<CoinController>(FindObjectsSortMode.None);
        System.Collections.Generic.HashSet<GameObject> uniqueCoins = new System.Collections.Generic.HashSet<GameObject>();
        foreach (CoinController coin in allCoinControllers)
        {
            uniqueCoins.Add(coin.gameObject);
        }
        totalCoins = uniqueCoins.Count;
        Debug.Log("Scene loaded: " + scene.name + " | Total coins: " + totalCoins);
        UpdateUI();
    }

    void Start()
    {
        // Only run if this is the persistent ScoreManager
        if (scoreManager != this) return;
        
        // Count unique GameObjects with CoinController to avoid counting duplicates
        CoinController[] allCoinControllers = FindObjectsByType<CoinController>(FindObjectsSortMode.None);
        System.Collections.Generic.HashSet<GameObject> uniqueCoins = new System.Collections.Generic.HashSet<GameObject>();
        foreach (CoinController coin in allCoinControllers)
        {
            uniqueCoins.Add(coin.gameObject);
        }
        totalCoins = uniqueCoins.Count;
        UpdateUI();
    }
    public void RaiseScore(int s)
    {
        score += s;
        Debug.Log("Score after collecting coin: " + score);
        UpdateUI();
    }

    void UpdateUI()
    {
        if (coinsCollected != null)
        {
            coinsCollected.text = "Bubbles: " + score + " / " + totalCoins;
        }
    }

    public bool AllCoinsCollected()
    {
        return score >= totalCoins;
    }

    public int GetScore()
    {
        return score;
    }
}
