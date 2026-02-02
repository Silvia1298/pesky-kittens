using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("Player")) return;
        if(ScoreManager.scoreManager.AllCoinsCollected())
        {
            //change scene
            SceneController.instance.NextLevel();
        }
        else
        {
            Debug.Log("You need to collect all the coins first");
        }
    }
}
