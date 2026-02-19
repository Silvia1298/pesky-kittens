using System.Collections;
using UnityEngine;

public class damage : MonoBehaviour
{ 
    public PlayerHealth playerHealth;
    public int dmg = 3;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerHealth.TakeDamage(dmg);

            SpriteRenderer sr = collision.gameObject.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                StartCoroutine(FlashRed(sr));
            }
        }
    }

    IEnumerator FlashRed(SpriteRenderer sr)
    {
        Color originalColor = sr.color;    // Guardar color original
        sr.color = Color.red;              // Poner rojo
        yield return new WaitForSeconds(0.2f); // Esperar 0.2 segundos
        sr.color = originalColor;          // Volver al color original
    }
}
