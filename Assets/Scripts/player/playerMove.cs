using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    float maxSpeed;
    float maxVSpeed = 15;
    float jumpPower;
    PlayerState state;
    Rigidbody2D rb;
    //private new BoxCollider2D collider;

    private int PlayerLayer, GroundLayer, EnemyLayer;
    int GroundMask;
    
    void Awake()
    {
        state = GetComponent<PlayerState>();
        rb = GetComponent<Rigidbody2D>();
        //collider = GetComponent<BoxCollider2D>();

        PlayerLayer = LayerMask.NameToLayer("PlayerLayer");
        GroundLayer = LayerMask.NameToLayer("GroundLayer");
        EnemyLayer = LayerMask.NameToLayer("EnemyLayer");
        GroundMask = LayerMask.GetMask("GroundLayer");

        maxSpeed = state.MovSpd;
        jumpPower = 10f;

        state.SetAnimBool("isJumping", true);
    }

    void Update()
    {
        // break speed with released key
        if(Input.GetButtonDown("Jump") ) //&& !state.GetAnimBool("isJumping")
        {
            RaycastHit2D hitBottom = Physics2D.Raycast(rb.transform.position, Vector2.down, 0.7f, GroundMask);
            if (Input.GetKey(KeyCode.DownArrow))
            {
                // 바닥 뚫기 방지용 코드
                if (hitBottom.collider && hitBottom.collider.name != "GroundM")
                {
                    rb.AddForce(Vector2.down * jumpPower / 3, ForceMode2D.Impulse);
                    state.SetAnimBool("isJumping", true);
                }
            }
            else
            {
                rb.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                state.SetAnimBool("isJumping", true);
            }
        }

        state.SetAnimBool("isWalking", Mathf.Abs(rb.velocity.x)>=0.1f);
    }

    void FixedUpdate()
    {
        //Move By key control
        float h = Input.GetAxisRaw("Horizontal");
        rb.AddForce(2 * h * Vector2.right, ForceMode2D.Impulse);

        // Max Speed
        if (rb.velocity.x > maxSpeed) rb.velocity = new Vector2(maxSpeed, rb.velocity.y);
        else if(rb.velocity.x < -maxSpeed) rb.velocity = new Vector2(-maxSpeed, rb.velocity.y);
        if (rb.velocity.y > maxVSpeed) rb.velocity = new Vector2(rb.velocity.x, maxVSpeed);
        else if (rb.velocity.y < -maxVSpeed) rb.velocity = new Vector2(rb.velocity.x, -maxVSpeed);

        //isJumping
        if (!state.GetAnimBool("isJumping")) rb.gravityScale = 0;
        else rb.gravityScale = 1;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            state.OnDamaged(other.transform.position);
        }

        if (other.gameObject.CompareTag("Ground"))
        {
            // 땅과 충돌한 경우 isGrounded를 true로 설정합니다.
            if (rb.velocity.y <= 0)
            {
                RaycastHit2D hitBottomLeft = Physics2D.Raycast((Vector2)rb.transform.position+(Vector2.left*0.3f), Vector2.down, 0.7f, GroundMask);
                RaycastHit2D hitBottom = Physics2D.Raycast(rb.transform.position, Vector2.down, 0.7f, GroundMask);
                RaycastHit2D hitBottomRight = Physics2D.Raycast((Vector2)rb.transform.position + (Vector2.right * 0.3f), Vector2.down, 0.7f, GroundMask);

                if (hitBottomLeft.collider || hitBottom.collider || hitBottomRight.collider)
                {
                    float addtionalPosition = 0.5f - Mathf.Max(hitBottomLeft.distance, hitBottom.distance, hitBottomRight.distance);
                    if (addtionalPosition < 0.3f)
                    {
                        rb.velocity = new Vector2(rb.velocity.x, 0);
                        rb.MovePosition(rb.position + new Vector2(0, addtionalPosition));
                        state.SetAnimBool("isJumping", false);
                    }
                    else
                    {
                        state.SetAnimBool("isJumping", true);
                    }
                }
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            // friction
            float friction = other.gameObject.GetComponent<Collider2D>().sharedMaterial.friction;
            Vector2 frictionForce = friction * Physics2D.gravity.magnitude * rb.mass * new Vector2(-rb.velocity.x, 0).normalized;
            rb.AddForce(frictionForce);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        RaycastHit2D hitBottomLeft = Physics2D.Raycast((Vector2)rb.transform.position + (Vector2.left * 0.3f), Vector2.down, 0.7f, GroundMask);
        RaycastHit2D hitBottom = Physics2D.Raycast(rb.transform.position, Vector2.down, 0.7f, GroundMask);
        RaycastHit2D hitBottomRight = Physics2D.Raycast((Vector2)rb.transform.position + (Vector2.right * 0.3f), Vector2.down, 0.7f, GroundMask);

        if (other.gameObject.CompareTag("Ground"))
        {
            state.SetAnimBool("isJumping", true);
        }
    }
}
