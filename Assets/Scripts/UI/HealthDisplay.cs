using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    public int health;
    public int maxHealth;

    public Sprite emptyHealth;
    public Sprite fullHealth;
    public Image[] hearts;

    public PlayerHealth playerHealth;
    void Start()
    {

    }

    void Update()
    {
        if (!playerHealth) return;

        health = playerHealth.health;
        maxHealth = playerHealth.maxHealth;

        for(int i = 0; i < hearts.Length; i++)
        {
            if(i< health)
            {
                hearts[i].sprite = fullHealth;
            }
            else
            {
                hearts[i].sprite = emptyHealth;
            }

            if(i < maxHealth)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }
}
