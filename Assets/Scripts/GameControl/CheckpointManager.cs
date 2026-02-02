using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    GameController gameController;
    public Transform respawnPoint;
    Collider2D coll;
    private void Awake()
    {
        coll = GetComponent<Collider2D>();
        gameController = GameObject.FindGameObjectWithTag("Player").GetComponent<GameController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            gameController.UpdateCheckpoint(respawnPoint.position);
            coll.enabled = false;
        }
    }

}
