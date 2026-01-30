using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    Vector2 checkpointPos;
    SpriteRenderer spriteRenderer;
    
    private void Start()
    {
        checkpointPos = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("WaterEdge"))
        {
            Die();
        }
    }

    void Die()
    {
        StartCoroutine(Respawn(0.5f));
    }

    public void UpdateCheckpoint(Vector2 pos)
    {
        checkpointPos = pos;
    }

    IEnumerator Respawn(float duration)
    {
        spriteRenderer.enabled = false;
        yield return new WaitForSeconds(duration);
        transform.position = checkpointPos; 
        spriteRenderer.enabled = true;
    }
}
