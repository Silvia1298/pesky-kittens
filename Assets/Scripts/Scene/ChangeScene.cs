using TMPro;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    public TMP_Text text;
    [TextArea] public string message;

    private Animator anim;

    private void Awake()
    {
        anim = text.GetComponent<Animator>();
        if (anim != null)
        {
            anim.Play("TextPopUp", 0, 0f);
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("Player")) return;
        if(ScoreManager.scoreManager.AllCoinsCollected())
        {
            //change scene
            SceneController.instance.NextLevel();
        }
        else
        {
            text.text = message;
            text.gameObject.SetActive(true);
            Animator anim = text.GetComponent<Animator>();
            if (anim != null)
            {
                anim.Play("TextPopUp", 0, 0f); // restart animation from beginning
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
