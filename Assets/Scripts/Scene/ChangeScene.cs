using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public TMP_Text text;
    [TextArea] public string message;

    private Animator anim;

    void Awake()
    {
        if (text != null)
            anim = text.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        string sceneName = SceneManager.GetActiveScene().name;

        bool canProceed = false;

        if (sceneName == "Scene1")
        {
            // Only coins needed
            canProceed = ScoreManager.scoreManager.AllCoinsCollected();
        }
        else if (sceneName == "Scene2")
        {
            // Coins AND balls needed
            canProceed = ScoreManager.scoreManager.AllCoinsCollected() 
                         && BallCount.ballCount.AllBallsCollected();
        }
        else
        {
            // Default: only coins
            canProceed = ScoreManager.scoreManager.AllCoinsCollected();
        }

        if (canProceed)
        {
            Time.timeScale = 1f; // ensure game isn't paused
            Debug.Log(SceneController.Instance);

            SceneController.Instance.NextLevel();
        }
        else
        {
            // Show message
            if (text != null)
            {
                text.text = message;
                text.gameObject.SetActive(true);
                if (anim != null)
                    anim.Play("TextPopUp", 0, 0f);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && text != null)
        {
            text.gameObject.SetActive(false);
        }
    }
}
