using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public PlayerHealth instance;
    public int damage = 1;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(player != null)
        {
            instance = player.GetComponent<PlayerHealth>();
        }
    }

    public void Attack()
    {
        if(instance != null)
        {
            instance.TakeDamage(damage);
        }   
    }

}
