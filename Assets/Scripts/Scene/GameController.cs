using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    SpriteRenderer spriteRenderer;
    private Dictionary<int, Vector2> sceneCheckpoints = new Dictionary<int, Vector2>();
    [SerializeField] private Transform defaultSpawn;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return;

        int sceneIndex = scene.buildIndex;

        // If we have a checkpoint for this scene, use it
        if (sceneCheckpoints.ContainsKey(sceneIndex))
        {
            player.transform.position = sceneCheckpoints[sceneIndex];
        }
        else if (defaultSpawn != null)
        {
            // First-time scene load → spawn at the default spawn
            player.transform.position = defaultSpawn.position;

            // Store this position as the "first checkpoint" if you want
            sceneCheckpoints[sceneIndex] = defaultSpawn.position;
        }
    }

    public void UpdateCheckpoint(Vector2 pos)
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        sceneCheckpoints[sceneIndex] = pos;
    }

    public void MovePlayerToCheckpoint(GameObject player)
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (sceneCheckpoints.ContainsKey(sceneIndex))
            player.transform.position = sceneCheckpoints[sceneIndex];
        // else first-time scene load → keep default spawn
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(collision.CompareTag("WaterEdge"))
        {
            Die(player);
        }
    }
    void Die(GameObject player)
    {
        StartCoroutine(Respawn(player));
    }

    IEnumerator Respawn(GameObject player)
    {
        spriteRenderer.enabled = false;
        yield return new WaitForSeconds(0.5f);
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (sceneCheckpoints.ContainsKey(sceneIndex))
        {
            // Respawn at the last checkpoint for this scene
            player.transform.position = sceneCheckpoints[sceneIndex];
        }
        else if (defaultSpawn != null)
        {
            // If no checkpoint, respawn at default spawn
            player.transform.position = defaultSpawn.position;
        }
        spriteRenderer.enabled = true;
    }
    
}


