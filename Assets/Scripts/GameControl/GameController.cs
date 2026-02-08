using System.Collections;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class GameController : MonoBehaviour
{
    Vector2 checkpointPos;
    public static GameController gameController;

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
        transform.position = checkpointPos;
        PlayerHealth.playerHealth.RestoreHealth();
        GetComponent<SpriteRenderer>().enabled = true;

    }


}
