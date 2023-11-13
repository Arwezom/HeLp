using System;
using UnityEngine;

public class PlayerMovement1 : MonoBehaviour
{
    float horizontalInput1;
    float moveSpeed1 = 12f;
    bool isFacingRight1 = false;

    Rigidbody2D rb1;
    Animator animator1;

    // Start is called before the first frame update
    void Start()
    {
        rb1 = GetComponent<Rigidbody2D>();
        animator1 = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput1 = Input.GetAxis("Horizontal");
        FlipSprite();
    }

    private void FixedUpdate()
    {
        rb1.velocity = new Vector2(horizontalInput1 * moveSpeed1, rb1.velocity.y);
        animator1.SetFloat("xVelocity", Math.Abs(rb1.velocity.x));
        animator1.SetFloat("yVelocity", rb1.velocity.y);
    }

    void FlipSprite()
    {
        if(isFacingRight1 && horizontalInput1 < 0f || !isFacingRight1 && horizontalInput1 > 0f)
        {
            isFacingRight1 = !isFacingRight1;
            Vector3 ls = transform.localScale;
            ls.x *= -1f;
            transform.localScale = ls;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.gameObject.CompareTag("Ground"))
        {
            
            animator1.SetBool("isJumping", false);
        } 
    }
        public void OnTriggerExit2D(Collider2D collision)
    {
       if(collision.gameObject.CompareTag("Ground"))
        {
            
            animator1.SetBool("isJumping", true);
        } 
    }
    
}