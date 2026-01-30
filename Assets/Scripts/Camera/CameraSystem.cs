using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraSystem : MonoBehaviour
{
    //scene management
    public static CameraSystem instance;
    public enum GameState {Loading, Active, Paused, Complete}
    [SerializeField] private GameObject playerPrefab;
    public GameState CurrentGameState {get; private set;}
    public static Action<GameState> onGameStateChanged;
    GameObject player; 
    private void Awake()
    {
        instance = this;
        player = Instantiate(playerPrefab, transform.position, Quaternion.identity);
    }
    private void Start()
    {
        SetGameState(GameState.Active);
    }

    public void SetGameState(GameState newState)
    {
        if(newState == CurrentGameState) return;
        
        switch(newState)
        {
            case GameState.Loading:   

            break;         
            case GameState.Active:      

            break;      
            case GameState.Paused:   

            break;         
            case GameState.Complete:    
                HandleComplete();
            break;        

        }

        CurrentGameState = newState;
        onGameStateChanged?.Invoke(newState);
    }
    private void HandleComplete() 
    { 
        int index = SceneManager.GetActiveScene().buildIndex; 
        int nextIndex = index + 1; 
        int previousIndex = index -1; 
    
       if(nextIndex < SceneManager.sceneCountInBuildSettings) 
       { 
        SceneManager.LoadScene(nextIndex); 
        player.transform.position = new Vector3(-40f, -9f, 0f); 
        } 
        else if(previousIndex < SceneManager.sceneCountInBuildSettings) 
        { 
            SceneManager.LoadScene(previousIndex); 
        player.transform.position = new Vector3(44f, 2f, 0f); 
        }
    }

     private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        CameraSystem.instance.SetGameState(CameraSystem.GameState.Complete);
        Debug.Log("Scene trigger hit by " + other.name);
    }

}   
