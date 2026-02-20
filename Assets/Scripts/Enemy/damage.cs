using UnityEngine;

public class damage : MonoBehaviour
{ 
    public PlayerHealth playerHealth;
    public int dmg = 3;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameController.killedByBoss = false; // Not killed by boss
            playerHealth.TakeDamage(dmg);
        }
    }
}
