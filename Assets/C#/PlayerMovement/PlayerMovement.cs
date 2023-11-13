using System.Collections;
using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float horizontalInput;
    float moveSpeed = 20f;
    bool isFacingRight = false;
    float jumpPower = 20f;
    int jumpValue = 2;
    bool isGrounded = false;

    public float dashDistance = 15f;
    bool isDashing;
    float doubleTapTime;
    KeyCode lastKeyCode;

    Rigidbody2D rb;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        StartCoroutine(CounterCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        FlipSprite();

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            Debug.Log("FirstJump"+jumpValue);
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            isGrounded = false;
            animator.SetBool("isJumping", !isGrounded);
            jumpValue--;
            StartCoroutine(CounterCoroutine());
            return;
        }


        if(Input.GetButtonDown("Jump") && jumpValue >= 1)
        {
            Debug.Log("SecondJump"+jumpValue);
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            jumpValue--;
            StartCoroutine(CounterCoroutine());
        }

        //dashing left
        if(Input.GetKeyDown(KeyCode.A) && jumpValue >= 1)
        {
            if(doubleTapTime > Time.time && lastKeyCode == KeyCode.A)
            {
                StartCoroutine(Dash(-5f));
            }
            else
            {
                doubleTapTime = Time.time + 1f;
            }
            lastKeyCode = KeyCode.A;
            jumpValue--;
            StartCoroutine(CounterCoroutine());
        }
        //dashing right
        if(Input.GetKeyDown(KeyCode.D) && jumpValue >= 1)
        {
            if(doubleTapTime > Time.time && lastKeyCode == KeyCode.D)
            {
                StartCoroutine(Dash(5f));
            }
            else
            {
                doubleTapTime = Time.time + 1f;
            }
            lastKeyCode = KeyCode.D;
            jumpValue--;
            StartCoroutine(CounterCoroutine());
        }
    }

    private void FixedUpdate()
    {
        //running
        if(!isDashing)
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
        animator.SetFloat("xVelocity", Math.Abs(rb.velocity.x));
        //jump
        animator.SetFloat("yVelocity", rb.velocity.y);
    }

    void FlipSprite()
    {
        if(isFacingRight && horizontalInput < 0f || !isFacingRight && horizontalInput > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 ls = transform.localScale;
            ls.x *= -1f;
            transform.localScale = ls;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Landen");
        jumpValue = 2;
        isGrounded = true;
        animator.SetBool("isJumping", !isGrounded);
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            
            animator.SetBool("isJumping", true);
        } 
    }
    IEnumerator Dash (float direction)
    {
        isDashing = true;
        rb.velocity = new Vector2(rb.velocity.x, 0f);
        rb.AddForce(new Vector2(dashDistance*direction, 0f), ForceMode2D.Impulse);
        float gravity = rb.gravityScale;
        rb.gravityScale = 0;
        yield return new WaitForSeconds(0.4f);
        isDashing = false;
        rb.gravityScale = gravity;

    }
    IEnumerator CounterCoroutine()
    {
        if(jumpValue == 0 && isGrounded)
        {
            jumpValue++;
            jumpValue++;
            yield return new WaitForSeconds(3);
        } 
        if(jumpValue == 1 && isGrounded)
        {
            jumpValue++;
            yield return new WaitForSeconds(3);
        }
        if(jumpValue >= 2 && isGrounded)
        {
            jumpValue--;
            yield return new WaitForSeconds(3);
        }

    }
}