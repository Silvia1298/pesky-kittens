using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    public void ToMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }
}