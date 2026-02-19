using UnityEngine;

public class Chase : MonoBehaviour
{
    bool isGrounded;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundCheckRadius = 0.2f;
    [SerializeField] LayerMask groundLayer;

    public GameObject player;
    public float speed;
    private float distance;

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        distance = Vector2.Distance(transform.position, player.transform.position);

        if(distance < 30 && isGrounded)
        {
            // Move only in X axis (prevents flying)
            float newX = Mathf.MoveTowards(
                transform.position.x,
                player.transform.position.x,
                speed * Time.deltaTime
            );

            transform.position = new Vector2(newX, transform.position.y);
        }
    }
}
