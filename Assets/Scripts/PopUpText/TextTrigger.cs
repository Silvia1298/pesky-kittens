using UnityEngine;
using TMPro;

public class TextTrigger : MonoBehaviour
{
    public static GameObject canvasObject;
    public static TextTrigger currentActiveTrigger;
    public TMP_Text tutorialText;
    public GameObject panelObject; // Reference to the panel that contains the text
    [TextArea] public string message;

    private Animator anim;
    private bool hasBeenTriggered = false;

    private void Awake()
    {
        if (canvasObject == null && tutorialText != null)
            canvasObject = tutorialText.transform.parent.parent.gameObject; // Canvas2

        if (tutorialText != null)
            anim = tutorialText.GetComponent<Animator>();
        
        // Auto-find panel if not assigned
        if (panelObject == null && tutorialText != null)
            panelObject = tutorialText.transform.parent.gameObject; // Panel is direct parent of text
    }

    private void Start()
    {
        // Ensure panel and text are hidden at start
        if (panelObject != null)
            panelObject.SetActive(false);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasBeenTriggered && tutorialText != null)
        {
            hasBeenTriggered = true;
            currentActiveTrigger = this;
            
            // Activate canvas if needed
            if (canvasObject != null)
                canvasObject.SetActive(true);
            
            // Activate panel and text
            if (panelObject != null)
                panelObject.SetActive(true);
            
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
            if (tutorialText != null)
                tutorialText.gameObject.SetActive(false);
            
            // Deactivate panel
            if (panelObject != null)
                panelObject.SetActive(false);
            
            if (canvasObject != null)
                canvasObject.SetActive(false);
            
            currentActiveTrigger = null;
        }
    }

}
