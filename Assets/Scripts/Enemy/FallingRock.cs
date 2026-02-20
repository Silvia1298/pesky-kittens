using UnityEngine;

public class FallingRock : MonoBehaviour
{
    public int damage = 3;
    public float lifetime = 8f;
    public float gravityScale = 0.3f;

    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.gravityScale = gravityScale;

        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth ph = other.GetComponent<PlayerHealth>();
            if (ph != null)
            {
                GameController.killedByBoss = true;
                ph.TakeDamage(damage);
            }

            Destroy(gameObject);
        }
        else if (!other.isTrigger && !other.CompareTag("Boss") && !other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth ph = collision.gameObject.GetComponent<PlayerHealth>();
            if (ph != null)
            {
                GameController.killedByBoss = true;
                ph.TakeDamage(damage);
            }
        }

        Destroy(gameObject);
    }
}
