using UnityEngine;

public class Hairball : MonoBehaviour
{
    public int damage = 3;
    public float lifetime = 3f;
    public bool firedByPlayer = true;
    public bool followPlayer = false; // For boss hairballs
    public float homingStrength = 5f; // How aggressively it follows

    private Transform player;
    private Rigidbody2D rb;

    void Start()
    {
        Destroy(gameObject, lifetime);
        rb = GetComponent<Rigidbody2D>();
        
        if (!firedByPlayer && followPlayer)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                player = playerObj.transform;
        }
    }

    void Update()
    {
        if (!firedByPlayer && followPlayer && player != null && rb != null)
        {
            // Calculate direction towards player
            Vector2 direction = (player.position - transform.position).normalized;
            
            // Smoothly adjust velocity towards player
            Vector2 targetVelocity = direction * rb.linearVelocity.magnitude;
            rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, targetVelocity, homingStrength * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (firedByPlayer)
        {
            if (other.CompareTag("Player")) return;

            if (other.CompareTag("Enemy") || other.CompareTag("Boss"))
            {
                DamageEnemy(other.gameObject);
                Destroy(gameObject);
                return;
            }
        }
        else
        {
            if (other.CompareTag("Enemy") || other.CompareTag("Boss")) return;

            if (other.CompareTag("Player"))
            {
                PlayerHealth ph = other.GetComponent<PlayerHealth>();
                if (ph != null)
                {
                    GameController.killedByBoss = true;
                    ph.TakeDamage(damage);
                }

                Destroy(gameObject);
                return;
            }
        }

        if (!other.isTrigger)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject obj = collision.gameObject;

        if (firedByPlayer)
        {
            if (obj.CompareTag("Player")) return;

            if (obj.CompareTag("Enemy") || obj.CompareTag("Boss"))
            {
                DamageEnemy(obj);
                Destroy(gameObject);
                return;
            }
        }
        else
        {
            if (obj.CompareTag("Enemy") || obj.CompareTag("Boss")) return;

            if (obj.CompareTag("Player"))
            {
                PlayerHealth ph = obj.GetComponent<PlayerHealth>();
                if (ph != null)
                {
                    GameController.killedByBoss = true;
                    ph.TakeDamage(damage);
                }
            }
        }

        Destroy(gameObject);
    }

    void DamageEnemy(GameObject enemy)
    {
        // Intentar dañar al boss
        BossController bc = enemy.GetComponent<BossController>();
        if (bc != null)
        {
            bc.TakeDamage(damage);
            return;
        }

        // Si no es boss, dañar al enemigo normal
        EnemyHealth eh = enemy.GetComponent<EnemyHealth>();
        if (eh != null)
        {
            eh.TakeDamage(damage);
            return;
        }
    }
}
