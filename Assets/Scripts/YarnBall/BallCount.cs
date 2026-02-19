using TMPro;
using UnityEngine;

public class BallCount : MonoBehaviour
{
    public TextMeshProUGUI ballCollected;
    public static BallCount ballCount;
    int score = 0;
    int total = 0;

    void Start()
    {
        ballCount = this;
        total = GameObject.FindGameObjectsWithTag("Ball").Length;
        UpdateUI();
    }
    public void RaiseScore(int s)
    {
        score += s;
        UpdateUI();
    }

    void UpdateUI()
    {
        ballCollected.text = "Yarn Count: " +score + " / " +total;
    }

    public bool AllBallsCollected()
    {
        return score >= total;
    }
}
