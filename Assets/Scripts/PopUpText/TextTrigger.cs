using UnityEngine;
using TMPro;

public class TextTrigger : MonoBehaviour
{
    public TMP_Text tutorialText;
    [TextArea] public string message;

    private Animator anim;

    private void Awake()
    {
        anim = tutorialText.GetComponent<Animator>();
        if (anim != null)
        {
            anim.Play("TextPopUp", 0, 0f);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            tutorialText.text = message;
            tutorialText.gameObject.SetActive(true);

            Animator anim = tutorialText.GetComponent<Animator>();
            if (anim != null)
            {
                anim.Play("TextPopUp", 0, 0f); // restart animation from beginning
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && tutorialText != null)
        {
            tutorialText.gameObject.SetActive(false);
        }
    }

}
