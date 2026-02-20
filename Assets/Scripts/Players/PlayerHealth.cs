using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public int maxHealth = 10;
    public static PlayerHealth playerHealth;
    private SpriteRenderer spriteRenderer;
    private Coroutine flashCoroutine;

    void Awake()
    {
        playerHealth = this;
        health = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        health = maxHealth;
    }

    void Start()
    {
        health = maxHealth; 
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        FlashDamage();
        if(health <= 0)
        {
            if (GameController.gameController != null)
            {
                GameController.gameController.Die();
            }
        }
    }

    public void FlashDamage()
    {
        if (spriteRenderer != null)
        {
            if (flashCoroutine != null)
                StopCoroutine(flashCoroutine);
            flashCoroutine = StartCoroutine(FlashRed());
        }
    }

    IEnumerator FlashRed()
    {
        if (spriteRenderer == null) yield break;
        
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        
        if (spriteRenderer != null)
            spriteRenderer.color = Color.white;
    }

    public void RestoreHealth()
    {
        health = maxHealth;
    }
}
