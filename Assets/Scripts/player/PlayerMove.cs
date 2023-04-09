using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    float maxSpeed;
    float maxVSpeed = 15;
    float jumpPower;
    PlayerState playerState;
    Rigidbody2D rigid;
    //private new BoxCollider2D collider;

    private int PlayerLayer, GroundLayer, EnemyLayer;
    int GroundMask;
    
    void Awake()
    {
        playerState = GetComponent<PlayerState>();
        rigid = GetComponent<Rigidbody2D>();
        //collider = GetComponent<BoxCollider2D>();

        PlayerLayer = LayerMask.NameToLayer("PlayerLayer");
        GroundLayer = LayerMask.NameToLayer("GroundLayer");
        EnemyLayer = LayerMask.NameToLayer("EnemyLayer");
        GroundMask = LayerMask.GetMask("GroundLayer");

        maxSpeed = playerState.MovSpd;
        jumpPower = 10f;

        playerState.SetAnimBool("isJumping", true);
    }

    void Update()
    {
        // break speed with released key
        if(Input.GetButtonDown("Jump") ) //&& !state.GetAnimBool("isJumping")
        {
            RaycastHit2D hitBottom = Physics2D.Raycast(rigid.transform.position, Vector2.down, 0.7f, GroundMask);
            if (Input.GetKey(KeyCode.DownArrow))
            {
                // 바닥 뚫기 방지용 코드
                if (hitBottom.collider && hitBottom.collider.name != "GroundM")
                {
                    rigid.AddForce(Vector2.down * jumpPower / 3, ForceMode2D.Impulse);
                    playerState.SetAnimBool("isJumping", true);
                }
            }
            else
            {
                rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                playerState.SetAnimBool("isJumping", true);
            }
        }

        playerState.SetAnimBool("isWalking", Mathf.Abs(rigid.velocity.x)>=0.1f);
    }

    void FixedUpdate()
    {
        //Move By key control
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(2 * h * Vector2.right, ForceMode2D.Impulse);

        // Max Speed
        if (rigid.velocity.x > maxSpeed) rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        else if(rigid.velocity.x < -maxSpeed) rigid.velocity = new Vector2(-maxSpeed, rigid.velocity.y);
        if (rigid.velocity.y > maxVSpeed) rigid.velocity = new Vector2(rigid.velocity.x, maxVSpeed);
        else if (rigid.velocity.y < -maxVSpeed) rigid.velocity = new Vector2(rigid.velocity.x, -maxVSpeed);

        //isJumping
        if (!playerState.GetAnimBool("isJumping")) rigid.gravityScale = 0;
        else rigid.gravityScale = 1;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            playerState.OnDamaged();
        }

        if (other.gameObject.CompareTag("Ground"))
        {
            // 땅과 충돌한 경우 isGrounded를 true로 설정합니다.
            if (rigid.velocity.y <= 0)
            {
                RaycastHit2D hitBottomLeft = Physics2D.Raycast((Vector2)rigid.transform.position+(Vector2.left*0.3f), Vector2.down, 0.7f, GroundMask);
                RaycastHit2D hitBottom = Physics2D.Raycast(rigid.transform.position, Vector2.down, 0.7f, GroundMask);
                RaycastHit2D hitBottomRight = Physics2D.Raycast((Vector2)rigid.transform.position + (Vector2.right * 0.3f), Vector2.down, 0.7f, GroundMask);

                if (hitBottomLeft.collider || hitBottom.collider || hitBottomRight.collider)
                {
                    float addtionalPosition = 0.5f - Mathf.Max(hitBottomLeft.distance, hitBottom.distance, hitBottomRight.distance);
                    if (addtionalPosition < 0.3f)
                    {
                        rigid.velocity = new Vector2(rigid.velocity.x, 0);
                        rigid.MovePosition(rigid.position + new Vector2(0, addtionalPosition));
                        playerState.SetAnimBool("isJumping", false);
                    }
                    else
                    {
                        playerState.SetAnimBool("isJumping", true);
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
            Vector2 frictionForce = friction * Physics2D.gravity.magnitude * rigid.mass * new Vector2(-rigid.velocity.x, 0).normalized;
            rigid.AddForce(frictionForce);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        RaycastHit2D hitBottomLeft = Physics2D.Raycast((Vector2)rigid.transform.position + (Vector2.left * 0.3f), Vector2.down, 0.7f, GroundMask);
        RaycastHit2D hitBottom = Physics2D.Raycast(rigid.transform.position, Vector2.down, 0.7f, GroundMask);
        RaycastHit2D hitBottomRight = Physics2D.Raycast((Vector2)rigid.transform.position + (Vector2.right * 0.3f), Vector2.down, 0.7f, GroundMask);

        if (other.gameObject.CompareTag("Ground"))
        {
            playerState.SetAnimBool("isJumping", true);
        }
    }
}
