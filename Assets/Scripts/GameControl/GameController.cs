using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class GameController : MonoBehaviour
{
    Vector2 checkpointPos;
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

    void Die()
    {
        Respawn();
    }

    public void UpdateCheckpoint(Vector2 pos)
    {
        checkpointPos = pos;
    }

    void Respawn()
    {
        transform.position = checkpointPos;
    }


}
