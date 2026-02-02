using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            //change scene
            SceneController.instance.NextLevel();
        }
    }
}
