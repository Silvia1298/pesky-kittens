using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public void Quit()
    {
        Debug.Log("Quit button pressed"); // should appear in Console
        Application.Quit();
    }

}