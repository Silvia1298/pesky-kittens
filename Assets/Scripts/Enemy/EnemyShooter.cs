using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    public GameObject hairballPrefab;
    public float shootInterval = 2f;
    public float shootForce = 8f;
    public float hairballScale = 10f;
    public float detectionRange = 20f;
    public int hairballDamage = 2;

    private Transform player;
    private SpriteRenderer spriteRenderer;
    private float lastShootTime;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        GameObject p = GameObject.FindWithTag("Player");
        if (p != null)
            player = p.transform;
    }

    void Update()
    {
        if (player == null) return;

        float dist = Vector2.Distance(transform.position, player.position);
        if (dist > detectionRange) return;

        // Follow player with gaze (flip sprite)
        bool playerIsLeft = player.position.x < transform.position.x;
        spriteRenderer.flipX = playerIsLeft;

        // Shoot at intervals
        if (Time.time >= lastShootTime + shootInterval)
        {
            Shoot(playerIsLeft);
            lastShootTime = Time.time;
        }
    }

    void Shoot(bool playerIsLeft)
    {
        if (hairballPrefab == null) return;

        float direction = playerIsLeft ? -1f : 1f;
        Vector3 spawnPos = transform.position + new Vector3(direction * 0.3f, 0f, 0f);

        GameObject hairball = Instantiate(hairballPrefab, spawnPos, Quaternion.identity);
        hairball.transform.localScale = Vector3.one * hairballScale;

        // Mark as enemy projectile
        Hairball hb = hairball.GetComponent<Hairball>();
        if (hb != null)
        {
            hb.firedByPlayer = false;
            hb.damage = hairballDamage;
        }

        Rigidbody2D rb = hairball.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = new Vector2(direction * shootForce, 0f);
        }
    }
}
