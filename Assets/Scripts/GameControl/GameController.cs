using System.Collections;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class GameController : MonoBehaviour
{
    Vector2 checkpointPos;
    public static GameController gameController;
    public static bool killedByBoss = false;

    void Awake()
    {
        gameController = this;
    }

    void Start()
    {
        checkpointPos = transform.position;
    }

//Die when touching water
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("WaterEdge"))
        {
            killedByBoss = false; // Not killed by boss
            Die();
        }
    }

    public void Die()
    {
        StartCoroutine(Respawn(0.5f));
        GetComponent<SpriteRenderer>().enabled = false;

    }

    public void UpdateCheckpoint(Vector2 pos)
    {
        checkpointPos = pos;
    }

    IEnumerator Respawn(float duration)
    {
        yield return new WaitForSeconds(duration);
        
        // Destroy all falling rocks when respawning
        FallingRock[] rocks = FindObjectsByType<FallingRock>(FindObjectsSortMode.None);
        foreach (FallingRock rock in rocks)
        {
            Destroy(rock.gameObject);
        }
        
        // Restore boss health if it killed the player
        if (killedByBoss)
        {
            BossController boss = FindFirstObjectByType<BossController>();
            if (boss != null)
            {
                boss.health = boss.maxHealth;
            }
            killedByBoss = false;
        }
        
        transform.position = checkpointPos;
        PlayerHealth.playerHealth.RestoreHealth();
        GetComponent<SpriteRenderer>().enabled = true;

    }


}
