using UnityEngine;

public class Hairball : MonoBehaviour
{
    public int damage = 3;
    public float lifetime = 3f;
    public bool firedByPlayer = true;

    void Start()
    {
        Destroy(gameObject, lifetime);
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
                    ph.TakeDamage(damage);

                FlashSprite(other.GetComponent<SpriteRenderer>());
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
            }
        }
        else
        {
            if (obj.CompareTag("Enemy") || obj.CompareTag("Boss")) return;

            if (obj.CompareTag("Player"))
            {
                PlayerHealth ph = obj.GetComponent<PlayerHealth>();
                if (ph != null)
                    ph.TakeDamage(damage);

                FlashSprite(obj.GetComponent<SpriteRenderer>());
            }
        }

        Destroy(gameObject);
    }

    void DamageEnemy(GameObject enemy)
    {
        EnemyHealth eh = enemy.GetComponent<EnemyHealth>();
        if (eh != null)
            eh.TakeDamage(damage);

        FlashSprite(enemy.GetComponent<SpriteRenderer>());
    }

    void FlashSprite(SpriteRenderer sr)
    {
        if (sr != null)
            StartCoroutine(FlashRed(sr));
    }

    System.Collections.IEnumerator FlashRed(SpriteRenderer sr)
    {
        Color original = sr.color;
        sr.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        if (sr != null)
            sr.color = original;
    }
}
