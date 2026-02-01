using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;


public class SceneTrigger : MonoBehaviour
{
    public static SceneTrigger instance;
    public enum GameState {Loading, Active, Paused, Complete}
    public GameState CurrentGameState {get; private set;}
    public static Action<GameState> onGameStateChanged;
    GameObject player; 
    private Collider2D col;

    public enum SceneDirection { Forward, Backward }
    public SceneDirection direction = SceneDirection.Forward;

    // Dictionary<sceneIndex, checkpointPosition>
    private Dictionary<int, Vector2> sceneCheckpoints = new Dictionary<int, Vector2>();

    public void UpdateCheckpoint(Vector2 pos)
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        sceneCheckpoints[sceneIndex] = pos; // store per scene
    }

    public void MovePlayerToCheckpoint(GameObject player)
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (sceneCheckpoints.ContainsKey(sceneIndex))
        {
            // Only move to checkpoint if this scene has one
            player.transform.position = sceneCheckpoints[sceneIndex];
        }
        // else first spawn in this scene → use default scene position
    }

    private void Awake()
    {
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player object not found");
        }

        col = GetComponent<Collider2D>();
        if (col == null)
        {
            Debug.LogError("SceneTrigger requires a Collider2D set as Trigger!");
        }
    }
    private void Start()
    {
        SetGameState(GameState.Active);
    }

    public void SetGameState(GameState newState)
    {
        if(newState == CurrentGameState) return;

        CurrentGameState = newState;
        onGameStateChanged?.Invoke(newState);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        if (GameController.instance == null) return;

        int prevIndex = SceneManager.GetActiveScene().buildIndex - 1;
        if (prevIndex >= 0)
        {
            SceneManager.LoadScene(prevIndex);
        }
        
    }

} 
