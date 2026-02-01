using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoinCheck : MonoBehaviour
{
    public int totalScore;
    public HashSet<string> collectedCoins = new HashSet<string>();
    public int totalCoinsInScene = 0;
    public static CoinCheck instance;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        CoinController[] coins = FindObjectsByType<CoinController>(
            FindObjectsInactive.Include,
            FindObjectsSortMode.None
        );

        foreach (var coin in coins)
        {
            // Disable coins that have already been collected
            if (collectedCoins.Contains(coin.coinID))
            {
                coin.gameObject.SetActive(false);
            }
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void CollectCoin(string coinID)
    {
        if(!collectedCoins.Contains(coinID))
        {
            collectedCoins.Add(coinID);
        }
    }

    public bool AnyCoinsLeftInScene()
    {
        CoinController[] coins = FindObjectsByType<CoinController>(
            FindObjectsInactive.Include,
            FindObjectsSortMode.None
        );

        foreach (var coin in coins)
        {
            if (!collectedCoins.Contains(coin.coinID))
                return true;
        }
        return false;
    }


    public bool AllCoinsCollected()
    {
        CoinController[] coins = FindObjectsByType<CoinController>(
            FindObjectsInactive.Include,
            FindObjectsSortMode.None
        );

        foreach (var coin in coins)
        {
            if (!collectedCoins.Contains(coin.coinID))
            {
                return false;
            } 
        }
        return true;
    }

    public void AddScore(int amount)
    {
        totalScore += amount;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        if (GameController.instance == null) return;

            // Forward progression only allowed if all coins collected
            if (!instance.AllCoinsCollected())
            {
                Debug.Log("You must collect all coins before proceeding!");
                return;
            }

            int nextIndex = SceneManager.GetActiveScene().buildIndex + 1;
            if (nextIndex < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(nextIndex);
            }
    }
}