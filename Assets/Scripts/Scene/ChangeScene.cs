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

    void Start()
    {
        // Make sure the message is hidden at start
        if (text != null)
        {
            // Don't deactivate anything, just make text invisible
            text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        string sceneName = SceneManager.GetActiveScene().name;

        bool canProceed = false;

        // Check if boss exists in this scene
        BossController boss = FindFirstObjectByType<BossController>();
        
        if (sceneName == "Scene3")
        {
            // Boss scene: only requirement is boss being defeated (either destroyed or health <= 0)
            if (boss != null)
            {
                canProceed = (boss.health <= 0);
            }
            else
            {
                // Boss was destroyed, so it was defeated
                canProceed = true;
            }
        }
        else if (ScoreManager.scoreManager != null)
        {
            // Non-boss scenes with ScoreManager: use original logic
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
        }
        else
        {
            // No boss and no ScoreManager, allow progression
            canProceed = true;
        }

        if (canProceed)
        {
            Time.timeScale = 1f; // ensure game isn't paused
            Debug.Log("Scene changed from " + sceneName + " | SceneController.Instance: " + SceneController.Instance);

            SceneController.Instance.NextLevel();
        }
        else
        {
            // Show message
            if (text != null)
            {
                // Ensure canvas is active
                if (TextTrigger.canvasObject != null)
                    TextTrigger.canvasObject.SetActive(true);
                    
                text.gameObject.SetActive(true);
                text.text = message;
                text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
                
                if (anim != null)
                    anim.Play("TextPopUp", 0, 0f);
                
                Debug.Log("Showing message: " + message);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && text != null)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
        }
    }
}
