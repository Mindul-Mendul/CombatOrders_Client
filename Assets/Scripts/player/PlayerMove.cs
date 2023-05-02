using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    bool chaos = false; // È¥¶õ
    bool faint = false; // ±âÀý
    bool slow = false; // µÐÈ­

    public bool Chaos { get => chaos; set => chaos = value; }
    public bool Faint { get => faint; set => faint = value; }
    public bool Slow { get => slow; set => slow = value; }

    public float MaxSpeed = 4f;
    public float MaxVSpeed = 15f;
    public float JumpPower = 10f;

    PlayerState playerState;

    Rigidbody2D rigid;
    //private new BoxCollider2D collider;
    Animator animator;

    private int PlayerLayer, GroundLayer, EnemyLayer;
    int GroundMask;
    
    void Awake()
    {
        animator = GetComponent<Animator>();
        playerState = GetComponent<PlayerState>();
        rigid = GetComponent<Rigidbody2D>();
        //collider = GetComponent<BoxCollider2D>();

        PlayerLayer = LayerMask.NameToLayer("PlayerLayer");
        GroundLayer = LayerMask.NameToLayer("GroundLayer");
        EnemyLayer = LayerMask.NameToLayer("EnemyLayer");
        GroundMask = LayerMask.GetMask("GroundLayer");

        animator.SetBool("isJumping", true);
    }

    void Update()
    {
        // break speed with released key
        if(!faint && Input.GetButtonDown("Jump") ) //&& !state.GetAnimBool("isJumping")
        {
            RaycastHit2D hitBottom = Physics2D.Raycast(rigid.transform.position, Vector2.down, 0.7f, GroundMask);
            if ((!chaos && Input.GetKey(KeyCode.DownArrow)) || (chaos && !Input.GetKey(KeyCode.DownArrow)))
            {
                // ¹Ù´Ú ¶Õ±â ¹æÁö¿ë ÄÚµå
                if (hitBottom.collider && hitBottom.collider.name != "GroundM")
                {
                    rigid.AddForce(Vector2.down * JumpPower / 3, ForceMode2D.Impulse);
                    animator.SetBool("isJumping", true);
                }
            }
            else
            {
                rigid.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse);
                animator.SetBool("isJumping", true);
            }
        }

        animator.SetBool("isWalking", Mathf.Abs(rigid.velocity.x)>=0.1f);

        // flip Sprite
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            GetComponent<SpriteRenderer>().flipX = (!chaos && Input.GetAxisRaw("Horizontal") == -1) || (chaos && Input.GetAxisRaw("Horizontal") == 1);
        }
    }

    void FixedUpdate()
    {
        //Move By key control
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(2 * h * (faint ? Vector2.right : Vector2.left), ForceMode2D.Impulse);

        // Max Speed
        float hspd = faint ? 0 : (slow ? MaxSpeed/4 : MaxSpeed);
        if (rigid.velocity.x > hspd) rigid.velocity = new Vector2(hspd, rigid.velocity.y);
        else if(rigid.velocity.x < -hspd) rigid.velocity = new Vector2(-hspd, rigid.velocity.y);
        if (rigid.velocity.y > MaxVSpeed) rigid.velocity = new Vector2(rigid.velocity.x, MaxVSpeed);
        else if (rigid.velocity.y < -MaxVSpeed) rigid.velocity = new Vector2(rigid.velocity.x, -MaxVSpeed);

        //isJumping
        if (!animator.GetBool("isJumping")) rigid.gravityScale = 0;
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
                        animator.SetBool("isJumping", false);
                    }
                    else
                    {
                        animator.SetBool("isJumping", true);
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
        if (other.gameObject.CompareTag("Ground"))
        {
            animator.SetBool("isJumping", true);
        }
    }
}
