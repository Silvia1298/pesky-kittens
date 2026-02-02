
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    bool isGrounded;
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundRadius = 0.07f;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float jumpForce = 5f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

//PLAYER MOVEMENT
    void Update()
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


        isGrounded = Physics2D.OverlapCircle(
        groundCheck.position,
        groundRadius,
        groundLayer
        );

//GROUNDCHECK
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        
        //Debug.Log("Grounded? " + isGrounded);
    }


}
