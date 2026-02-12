using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI coinsCollected;
    public static ScoreManager scoreManager;
    int score = 0;
    int totalCoins = 0;

    void Start()
    {
        scoreManager = this;
        totalCoins = GameObject.FindGameObjectsWithTag("Coin").Length;
        UpdateUI();
    }
    public void RaiseScore(int s)
    {
        score += s;
        UpdateUI();
    }

    void UpdateUI()
    {
        coinsCollected.text = "Bubbles: " +score + " / " +totalCoins;
    }

    public bool AllCoinsCollected()
    {
        return score >= totalCoins;
    }
}
