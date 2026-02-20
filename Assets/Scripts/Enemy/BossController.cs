using System.Collections;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [Header("References")]
    public GameObject hairballPrefab;
    public GameObject rockPrefab;
    public AudioClip evilMeowSound;

    [Header("General")]
    public float aggroRange = 100f;
    public float attackCooldown = 3.5f;
    public float hairballSpeed = 25f;
    public float hairballScale = 10f;

    [Header("Jump Attack")]
    public float jumpHeightMultiplier = 2.5f;
    public float jumpDuration = 1.2f;
    public int jumpDamage = 5;

    [Header("Rock Attack")]
    public int rockCount = 5;
    public float rockHeightMultiplier = 3f;

    [Header("Health")]
    public int maxHealth = 30;
    public int health;

    private Transform player;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private float nextAttackTime;
    private bool isAttacking;
    private int lastAttack = -1;
    private float bossHeight;
    private Coroutine flashCoroutine;

    // Force position override so Animator can't snap us back
    private bool forcePosition;
    private Vector3 forcedPos;

    Vector3 CurrentPos => forcePosition ? forcedPos : transform.position;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        health = maxHealth;

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;

        bossHeight = spriteRenderer.bounds.size.y;
        nextAttackTime = Time.time + 2f;
    }

    void LateUpdate()
    {
        // Override position AFTER Animator writes to it
        if (forcePosition)
            transform.position = forcedPos;
    }

    void Update()
    {
        if (player == null || isAttacking) return;

        float yDiff = Mathf.Abs(player.position.y - CurrentPos.y);
        if (yDiff > bossHeight) return;

        float distanceX = Mathf.Abs(player.position.x - CurrentPos.x);
        if (distanceX > aggroRange) return;

        FacePlayer();

        if (Time.time >= nextAttackTime)
        {
            int attack = Random.Range(0, 3);
            while (attack == lastAttack)
                attack = Random.Range(0, 3);
            lastAttack = attack;

            StartCoroutine(DoAttack(attack));
        }
    }

    void FacePlayer()
    {
        if (player == null) return;
        spriteRenderer.flipX = player.position.x < CurrentPos.x;
    }

    IEnumerator DoAttack(int attack)
    {
        isAttacking = true;

        switch (attack)
        {
            case 0: yield return StartCoroutine(HairballAttack()); break;
            case 1: yield return StartCoroutine(JumpAttack()); break;
            case 2: yield return StartCoroutine(MeowRockAttack()); break;
        }

        isAttacking = false;
        nextAttackTime = Time.time + attackCooldown;
    }

    IEnumerator HairballAttack()
    {
        animator.SetTrigger("throw");
        yield return new WaitForSeconds(0.4f);

        if (hairballPrefab != null && player != null)
        {
            float direction = player.position.x > CurrentPos.x ? 1f : -1f;
            Vector3 spawnPos = new Vector3(
                CurrentPos.x + direction * 5f,
                CurrentPos.y,
                0f
            );

            GameObject hairball = Instantiate(hairballPrefab, spawnPos, Quaternion.identity);
            hairball.transform.localScale = Vector3.one * hairballScale;

            Hairball hb = hairball.GetComponent<Hairball>();
            if (hb != null)
            {
                hb.firedByPlayer = false;
                hb.followPlayer = true; // Enable homing
            }

            Rigidbody2D rb = hairball.GetComponent<Rigidbody2D>();
            if (rb != null)
                rb.linearVelocity = new Vector2(direction * hairballSpeed, 0f);

            SpriteRenderer hairballSR = hairball.GetComponent<SpriteRenderer>();
            if (hairballSR != null && direction < 0)
                hairballSR.flipX = true;
        }

        yield return new WaitForSeconds(0.5f);
    }

    IEnumerator JumpAttack()
    {
        float targetX = player != null ? player.position.x : CurrentPos.x;
        Vector3 origin = CurrentPos;
        float jumpArc = bossHeight * jumpHeightMultiplier;

        animator.SetTrigger("jump");
        yield return new WaitForSeconds(0.3f);

        // Temporarily disable force so we control position in the loop
        forcePosition = false;

        float elapsed = 0f;
        while (elapsed < jumpDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / jumpDuration);

            float x = Mathf.Lerp(origin.x, targetX, t);
            float arc = jumpArc * 4f * t * (1f - t);

            forcedPos = new Vector3(x, origin.y + arc, origin.z);
            forcePosition = true;

            yield return null;
        }

        // Land at player X, keep own Y
        forcedPos = new Vector3(targetX, origin.y, origin.z);
        forcePosition = true;

        // Deal damage if near player
        if (player != null)
        {
            float dist = Vector2.Distance(forcedPos, player.position);
            if (dist < bossHeight)
            {
                PlayerHealth ph = player.GetComponent<PlayerHealth>();
                if (ph != null)
                {
                    GameController.killedByBoss = true;
                    ph.TakeDamage(jumpDamage);
                }
            }
        }

        yield return new WaitForSeconds(0.5f);
    }

    IEnumerator MeowRockAttack()
    {
        animator.SetTrigger("meow");

        // Find AudioManager if it's null
        if (AudioManager.instance == null)
        {
            AudioManager.instance = FindFirstObjectByType<AudioManager>();
        }
        
        if (evilMeowSound != null)
        {
            if (AudioManager.instance != null)
            {
                AudioManager.instance.PlaySound(evilMeowSound);
            }
            else
            {
                // Fallback: find any AudioSource and use it
                AudioSource[] audioSources = FindObjectsByType<AudioSource>(FindObjectsSortMode.None);
                if (audioSources.Length > 0)
                {
                    audioSources[0].PlayOneShot(evilMeowSound);
                }
            }
        }

        yield return new WaitForSeconds(0.5f);

        if (rockPrefab != null)
        {
            for (int i = 0; i < rockCount; i++)
            {
                // Stop spawning rocks if player is dead or null
                if (player == null) break;
                
                PlayerHealth ph = player.GetComponent<PlayerHealth>();
                if (ph != null && ph.health <= 0) break;
                
                Vector3 spawnPos = new Vector3(
                    player.position.x,
                    player.position.y + (bossHeight * rockHeightMultiplier),
                    0f
                );
                Instantiate(rockPrefab, spawnPos, Quaternion.identity);
                yield return new WaitForSeconds(0.5f);
            }
        }

        yield return new WaitForSeconds(0.5f);
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        FlashDamage();
        if (health <= 0)
        {
            Die();
        }
    }

    void FlashDamage()
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

    void Die()
    {
        Debug.Log("Boss defeated! Health: " + health);
        Destroy(gameObject);
    }
}
