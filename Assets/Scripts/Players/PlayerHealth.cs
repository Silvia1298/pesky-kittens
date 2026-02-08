using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public int maxHealth = 10;
    public static PlayerHealth playerHealth;

    void Awake()
    {
        playerHealth = this;
    }

    void Start()
    {
        health= maxHealth; 
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if(health <= 0)
        {
            GameController.gameController.Die();
        }
    }
    public void RestoreHealth()
    {
        health = maxHealth;
    }
}
