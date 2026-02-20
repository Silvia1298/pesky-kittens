using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 20;
    private SpriteRenderer spriteRenderer;
    private Coroutine flashCoroutine;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        FlashDamage();
        if (health <= 0)
        {
            Die();
        }
    }

    void FlashDamage()
    {
        if (spriteRenderer != null)
        {
            if (flashCoroutine != null)
                StopCoroutine(flashCoroutine);
            flashCoroutine = StartCoroutine(FlashRed());
        }
    }

    IEnumerator FlashRed()
    {
        if (spriteRenderer == null) yield break;
        
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        
        if (spriteRenderer != null)
            spriteRenderer.color = Color.white;
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
