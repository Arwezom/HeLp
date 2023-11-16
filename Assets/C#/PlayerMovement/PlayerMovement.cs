using System.Collections;
using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float horizontalInput;
    float moveSpeed = 20f;
    bool isFacingRight = false;
    float jumpPower = 20f;
    int jumpValue1 = 0;
    int jumpValue2 = 0;
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
        jumpValue1++;
        jumpValue2++;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        FlipSprite();

        if(Input.GetButtonDown("Jump") && isGrounded && jumpValue1 == 1) 
        {
            Debug.Log("FirstJump"+jumpValue1);
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            isGrounded = false;
            animator.SetBool("isJumping", !isGrounded);
            jumpValue1--;
            StartCoroutine(jump1Coroutine());
            return;
        }


        if(Input.GetButtonDown("Jump") && jumpValue2 == 1)
        {
            Debug.Log("SecondJump"+jumpValue2);
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            jumpValue2--;
            StartCoroutine(jump2Coroutine());
        }

        //dashing left
        if(Input.GetKeyDown(KeyCode.A) && (jumpValue1 == 1 || jumpValue2 == 1))
        {
            if(doubleTapTime > Time.time && lastKeyCode == KeyCode.A)
            {
                if(jumpValue1 == 1)
                {
                    jumpValue1--;
                    StartCoroutine(Dash(-2.5f));
                    StartCoroutine(jump1Coroutine());
                }
                else
                {
                    jumpValue2--;
                    StartCoroutine(Dash(-2.5f));
                    StartCoroutine(jump2Coroutine());
                }
            }
            else
            {
                doubleTapTime = Time.time + 1f;
            }
            lastKeyCode = KeyCode.A;
            return;
        }

        //dashing right
        if(Input.GetKeyDown(KeyCode.D) && (jumpValue1 == 1 || jumpValue2 == 1))
        {
            if(doubleTapTime > Time.time && lastKeyCode == KeyCode.D)
            {
                if(jumpValue1 == 1)
                {
                    jumpValue1--;
                    StartCoroutine(Dash(2.5f));
                    StartCoroutine(jump1Coroutine());
                }
                else
                {
                    jumpValue2--;
                    StartCoroutine(Dash(2.5f));
                    StartCoroutine(jump2Coroutine());
                    
                }
            }
            else
            {
                doubleTapTime = Time.time + 1f;
            }
            lastKeyCode = KeyCode.D;


            StartCoroutine(jump1Coroutine());
            StartCoroutine(jump2Coroutine());
        }
        return;
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
        yield return new WaitForSeconds(0.4f);
        isDashing = false;

    }
    IEnumerator jump1Coroutine()
    {   
        if(jumpValue1 <= 0)
        {
            yield return new WaitForSeconds(3);
            jumpValue1 = 1;
        }
    }
    IEnumerator jump2Coroutine()
    {   
        if(jumpValue2 <= 0)
        {
            yield return new WaitForSeconds(3);
            jumpValue2 = 1;
        }
    }
}