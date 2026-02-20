using UnityEngine;

public class HairballAbility : MonoBehaviour
{
    public bool hasAbility = false;
    public GameObject hairballPrefab;
    public float shootForce = 10f;
    public float cooldown = 0.5f;
    public float hairballScale = 10f;
    public AudioClip shootSound;

    private float lastShootTime;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!hasAbility) return;

        if (Input.GetKeyDown(KeyCode.R) && Time.time >= lastShootTime + cooldown)
        {
            Shoot();
            lastShootTime = Time.time;
        }
    }

    void Shoot()
    {
        if (hairballPrefab == null) return;

        // Direction based on where the player is facing
        float direction = spriteRenderer.flipX ? -1f : 1f;

        // Spawn slightly in front of the player
        Vector3 spawnPos = transform.position + new Vector3(direction * 0.3f, 0.05f, 0f);

        GameObject hairball = Instantiate(hairballPrefab, spawnPos, Quaternion.identity);
        hairball.transform.localScale = Vector3.one * hairballScale;
        Rigidbody2D rb = hairball.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = new Vector2(direction * shootForce, 0f);
        }

        // Flip the hairball sprite if shooting left
        SpriteRenderer hairballSR = hairball.GetComponent<SpriteRenderer>();
        if (hairballSR != null && direction < 0)
        {
            hairballSR.flipX = true;
        }

        // Try to play sound
        if (shootSound != null)
        {
            // Find AudioManager if it's null
            if (AudioManager.instance == null)
            {
                AudioManager.instance = FindFirstObjectByType<AudioManager>();
            }
            
            // If still null, try to find an AudioSource directly
            if (AudioManager.instance != null)
            {
                AudioManager.instance.PlaySound(shootSound);
            }
            else
            {
                Debug.LogWarning("HairballAbility: Could not find AudioManager. Trying to play sound directly.");
                AudioSource[] audioSources = FindObjectsByType<AudioSource>(FindObjectsSortMode.None);
                if (audioSources.Length > 0)
                {
                    audioSources[0].PlayOneShot(shootSound);
                }
            }
        }
    }

    public void UnlockAbility()
    {
        hasAbility = true;
    }
}
