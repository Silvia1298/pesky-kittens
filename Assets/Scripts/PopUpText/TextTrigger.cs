using UnityEngine;
using TMPro;

public class TextTrigger : MonoBehaviour
{
    public static GameObject textPanel;
    public static TextTrigger currentActiveTrigger;
    public TMP_Text tutorialText;
    [TextArea] public string message;

    private Animator anim;
    private bool hasBeenTriggered = false;

    private void Awake()
    {
        if (textPanel == null)
            textPanel = tutorialText.transform.parent.gameObject;

        anim = tutorialText.GetComponent<Animator>();
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasBeenTriggered)
        {
            hasBeenTriggered = true;
            currentActiveTrigger = this;
            textPanel.SetActive(true);
            tutorialText.text = message;
            tutorialText.gameObject.SetActive(true);

            if (anim != null)
            {
                anim.Play("TextPopUp", 0, 0f);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && currentActiveTrigger == this)
        {
            tutorialText.gameObject.SetActive(false);
            textPanel.SetActive(false);
            currentActiveTrigger = null;
        }
    }

}
