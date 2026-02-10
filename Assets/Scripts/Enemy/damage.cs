using UnityEngine;

public class damage : MonoBehaviour
{ 
    public PlayerHealth playerHealth;
    public int dmg = 3;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("Player hit!");
            playerHealth.TakeDamage(dmg);
        }
    }
}
