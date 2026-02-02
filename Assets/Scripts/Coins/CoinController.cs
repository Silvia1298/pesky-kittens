using UnityEngine;

public class CoinController : MonoBehaviour
{
    public AudioClip coinSound;
    public string coinID; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("Player")) return;
 
        ScoreManager.scoreManager.RaiseScore(1);
        AudioManager.instance.PlaySound(coinSound);
        gameObject.SetActive(false);
        
    }
}
