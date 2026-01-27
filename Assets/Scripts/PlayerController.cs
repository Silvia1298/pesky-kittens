using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    bool canJump;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {}

    // Update is called once per frame
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

        if(Input.GetKeyDown("up") && canJump)
        {
            canJump = false;
             gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 200));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "ground")
        {
            canJump = true;
        }
    
    }
}
