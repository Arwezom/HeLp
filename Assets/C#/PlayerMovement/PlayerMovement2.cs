using System;
using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
{
    float horizontalInput2;
    float moveSpeed2 = 12f;
    bool isFacingRight2 = false;
    float jumpPower2 = 20f;
    bool isGrounded2;

    Rigidbody2D rb2;
    Animator animator2;

    // Start is called before the first frame update
    void Start()
    {
        rb2 = GetComponent<Rigidbody2D>();
        animator2 = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput2 = Input.GetAxis("Horizontal");
        FlipSprite();
           if(Input.GetButtonDown("Jump") && isGrounded2)
        {
            rb2.velocity = new Vector2(rb2.velocity.x, jumpPower2);
            isGrounded2 = false;
            animator2.SetBool("isJumping", !isGrounded2);
        }
    }

    private void FixedUpdate()
    {
        rb2.velocity = new Vector2(horizontalInput2 * moveSpeed2, rb2.velocity.y);
        animator2.SetFloat("xVelocity", Math.Abs(rb2.velocity.x));
        animator2.SetFloat("yVelocity", rb2.velocity.y);
    }

    void FlipSprite()
    {
        if(isFacingRight2 && horizontalInput2 < 0f || !isFacingRight2 && horizontalInput2 > 0f)
        {
            isFacingRight2 = !isFacingRight2;
            Vector3 ls = transform.localScale;
            ls.x *= -1f;
            transform.localScale = ls;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isGrounded2 = true;
        animator2.SetBool("isJumping", !isGrounded2);
    }
          public void OnTriggerExit2D(Collider2D collision)
    {
       if(collision.gameObject.CompareTag("Ground"))
        {
            
            animator2.SetBool("isJumping", true);
        } 
    }
    
}