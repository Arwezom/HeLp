using System;
using UnityEngine;

public class PlayerMovement1 : MonoBehaviour
{
    float horizontalInput1;
    float moveSpeed1 = 15f;
    bool isFacingRight1 = false;
    bool isGrounded1 = false;

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
        if(isGrounded1 == false)
        {
            animator1.SetBool("isJumping", !isGrounded1);
        }
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isGrounded1 = true;
        animator1.SetBool("isJumping", !isGrounded1);
    }
}