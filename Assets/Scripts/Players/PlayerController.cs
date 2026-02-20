using UnityEngine;

public class PlayerController : MonoBehaviour
{
    bool isGrounded;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundRadius = 0.07f;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float jumpForce = 5f;
    int jumpCount = 0;
    private Rigidbody2D rb;
    public static bool movementLocked = false;
    public static bool jumpLocked = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

//PLAYER MOVEMENT
    void Update()
    {
        if (!movementLocked)
        {
            if(Input.GetKey("left")) 
            {
                gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-500 * Time.deltaTime, 0));
                gameObject.GetComponent<Animator>().SetBool("moving", true); 
                gameObject.GetComponent<SpriteRenderer>().flipX = true; 
            }
            if(Input.GetKey("right")) 
            { 
                gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(500 * Time.deltaTime, 0));
                gameObject.GetComponent<Animator>().SetBool("moving", true); 
                gameObject.GetComponent<SpriteRenderer>().flipX = false; 
            }

            if(!Input.GetKey("left") && !Input.GetKey("right"))
            {
                 gameObject.GetComponent<Animator>().SetBool("moving", false);
            }
        }
        else
        {
            gameObject.GetComponent<Animator>().SetBool("moving", false);
        }


        isGrounded = Physics2D.OverlapCircle(
        groundCheck.position,
        groundRadius,
        groundLayer
        );

//GROUNDCHECK
        if (!jumpLocked && Input.GetKeyDown(KeyCode.Space) && (isGrounded || jumpCount < 1))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpCount++;
        }
        if (isGrounded)
        {
            jumpCount = 0;
        }
    }


}
