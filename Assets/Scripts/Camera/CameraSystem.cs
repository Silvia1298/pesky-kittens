using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraSystem : MonoBehaviour
{
    //scene management
    public static CameraSystem instance;
    public enum GameState {Loading, Active, Paused, Complete}
    public GameState CurrentGameState {get; private set;}
    public static Action<GameState> onGameStateChanged;
    private void Awake()
    {
        instance = this;
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

        if(nextIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextIndex);
        }

    }

     private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        CameraSystem.instance.SetGameState(CameraSystem.GameState.Complete);
        Debug.Log("Scene trigger hit by " + other.name);
    }

}   
