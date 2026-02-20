using UnityEngine;

public class HairballPickup : MonoBehaviour
{
    public float interactRange = 4f;
    public GameObject promptUI; // Optional: the "E" prompt visual

    private Transform player;
    private bool collected = false;

    void Start()
    {
        GameObject p = GameObject.FindWithTag("Player");
        if (p != null)
            player = p.transform;
    }

    void Update()
    {
        if (collected || player == null) return;

        float dist = Vector2.Distance(player.position, transform.position);

        // Lock/unlock player movement and jump based on distance
        if (dist <= interactRange)
        {
            PlayerController.movementLocked = true;
            PlayerController.jumpLocked = true;
        }
        else
        {
            PlayerController.movementLocked = false;
            PlayerController.jumpLocked = false;
        }

        // Show/hide prompt
        if (promptUI != null)
        {
            promptUI.SetActive(dist <= interactRange);
        }

        if (dist <= interactRange && Input.GetKeyDown(KeyCode.E))
        {
            collected = true;
            PlayerController.movementLocked = false;
            PlayerController.jumpLocked = false;

            // Enable hairball ability on the player
            HairballAbility ability = player.GetComponent<HairballAbility>();
            if (ability != null)
            {
                ability.UnlockAbility();
            }

            // Hide the prompt
            if (promptUI != null)
            {
                promptUI.SetActive(false);
            }
        }
    }
}
