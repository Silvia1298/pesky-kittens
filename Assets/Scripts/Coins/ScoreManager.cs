using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI coinsCollected;
    public static ScoreManager scoreManager;
    int score = 0;

    void Start()
    {
        scoreManager = this;
    }
    public void RaiseScore(int s)
    {
        score += s;
        coinsCollected.text = "Coins: " + score;
    }
}
