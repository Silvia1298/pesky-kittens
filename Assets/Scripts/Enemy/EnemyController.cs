using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject player;
    EnemyDamage enemyDamage;
    public int health = 10;
    public int maxHealth = 10;
    private Animator animator;

    public float attackRange = 30f;
    public float attackCooldown = 1.5f;
    public float speed = 2f;
    Rigidbody2D rb;
    Vector2 target;

    bool isAttacking = false;


    void Start()
    {
        health = maxHealth;
        enemyDamage = GetComponent<EnemyDamage>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
         rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if(player == null || rb == null) return;

        if(!isAttacking)
        {
            target = new Vector2(player.transform.position.x, transform.position.y);
            rb.MovePosition(Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime));
        }
    }


    void Update()
    {
        if(player == null || enemyDamage == null) return;
        if(isAttacking) return;

        Bounds enemyBounds = GetComponent<Collider2D>().bounds;
        Bounds playerBounds = player.GetComponent<Collider2D>().bounds;

        // Horizontal
        float xDistance = Mathf.Abs(playerBounds.center.x - enemyBounds.center.x) - (playerBounds.extents.x + enemyBounds.extents.x);

        // Vertical
        float yDistance = Mathf.Abs(playerBounds.center.y - enemyBounds.center.y) - (playerBounds.extents.y + enemyBounds.extents.y);

        float verticalAttackTolerance = 3f;

        if(!isAttacking)
        {
            if(xDistance <= attackRange && yDistance <= verticalAttackTolerance)
            {
                StartCoroutine(DoAttack());
            }

            // Flip
            Vector3 scale = transform.localScale;
            scale.x = player.transform.position.x > transform.position.x
                ? Mathf.Abs(scale.x)
                : -Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
          Debug.Log(xDistance + " | " + yDistance);
    }

    IEnumerator DoAttack()
    {
        isAttacking = true;

        // Lanzar trigger
        animator.SetTrigger("Attack");

        // Daño
        enemyDamage.Attack();

        yield return new WaitForSeconds(attackCooldown);

        isAttacking = false;
    }

    public void TakeDmg(int amount)
    {
        health -= amount;
        if(health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Animación de muerte
        if(animator != null)
            animator.SetTrigger("death");

        Destroy(gameObject, 0.5f);
    }

}
