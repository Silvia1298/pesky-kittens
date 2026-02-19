using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ThrowBall : MonoBehaviour
{
    public int requiredHits = 10;
    public float interactRange = 4f;
    private Transform player;
    private Animator animator;
    public Button button; 

    public float buttonPressDuration = 0.2f; // How long the button looks pressed

    private int scoreAmount = 0;
    private bool isThrown = false;

    private Image buttonImage;
    private Color normalColor;
    private Color pressedColor;

    void Start()
    {
        GameObject p = GameObject.FindWithTag("Player");
        if (p != null)
            player = p.transform;

        animator = GetComponent<Animator>();

        // Initialize button colors
        if (button != null)
        {
            buttonImage = button.GetComponent<Image>();
            normalColor = buttonImage.color;
            pressedColor = button.colors.pressedColor;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isThrown)
        {
            if (player != null)
            {
                float dist = Vector3.Distance(player.position, transform.position);

                if (dist <= interactRange)
                {
                    scoreAmount++;

                    // Call the button function
                    if (button != null)
                        button.onClick.Invoke();

                    // Flash the button visually
                    if (buttonImage != null)
                        StartCoroutine(FlashButton());

                    if (scoreAmount >= requiredHits)
                    {
                        isThrown = true; 
                        if (animator != null)
                            animator.SetBool("isThrown", true);

                        BallCount.ballCount.RaiseScore(1);

                        Destroy(gameObject, 1f);
                    }
                }
            }
        }
    }

    // Coroutine to show button as pressed
    private IEnumerator FlashButton()
    {
        buttonImage.color = pressedColor;
        yield return new WaitForSeconds(buttonPressDuration);
        buttonImage.color = normalColor;
    }
}
