using System;
using UnityEngine;

public class PlayerMovement3 : MonoBehaviour
{
    float horizontalInput3;
    float moveSpeed3 = 12f;
    bool isFacingRight3 = false;
    float jumpPower3 = 20f;
    bool isGrounded3;

    Rigidbody2D rb3;
    Animator animator3;

    // Start is called before the first frame update
    void Start()
    {
        rb3 = GetComponent<Rigidbody2D>();
        animator3 = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput3 = Input.GetAxis("Horizontal");
        FlipSprite();
    }

    private void FixedUpdate()
    {
        rb3.velocity = new Vector2(horizontalInput3 * moveSpeed3, rb3.velocity.y);
        animator3.SetFloat("xVelocity", Math.Abs(rb3.velocity.x));
        animator3.SetFloat("yVelocity", rb3.velocity.y);
    }

    void FlipSprite()
    {
        if(isFacingRight3 && horizontalInput3 < 0f || !isFacingRight3 && horizontalInput3 > 0f)
        {
            isFacingRight3 = !isFacingRight3;
            Vector3 ls = transform.localScale;
            ls.x *= -1f;
            transform.localScale = ls;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.gameObject.CompareTag("Ground"))
        {
            
            animator3.SetBool("isJumping", false);
        } 
    }
        public void OnTriggerExit2D(Collider2D collision)
    {
       if(collision.gameObject.CompareTag("Ground"))
        {
            
            animator3.SetBool("isJumping", true);
        } 
    }
    
}