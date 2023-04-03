using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;
    GameObject prfHPbar;
    GameObject canvas;
    RectTransform MaxHPbar;
    Image HPbar;
    bool isGrounded;

    public delegate void OnDeathDelegate();
    public event OnDeathDelegate OnDeath;

    int curMove = 0;
    public readonly int enemySpeed = 2;
    float nextThinkTerm = 3f;

    int HP = 100;
    public readonly int MaxHP = 100;
    public readonly float HPHeight = 0.7f;
    public int expValue;

    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        prfHPbar = GameObject.Find("EnemyHPbar_bg");
        canvas = GameObject.Find("Canvas");
        MaxHPbar = Instantiate(prfHPbar, canvas.transform).GetComponent<RectTransform>();
        HPbar = MaxHPbar.transform.GetChild(0).GetComponent<Image>();

        // Think the next move
        nextThinkTerm = Random.Range(3f, 5f);
        Invoke(nameof(Think), nextThinkTerm);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rigid.velocity = new Vector2(curMove*enemySpeed, rigid.velocity.y);

        spriteRenderer.flipX = (curMove == 1);
        anim.SetBool("isWalking", (Mathf.Abs(curMove) == 1));

        if (isGrounded)
        {
            // 땅 위에 있으면 Rigidbody2D의 중력을 0으로 설정하여 땅에 고정시킵니다.
            rigid.gravityScale = 0;
        }
        else
        {
            // 땅 위에 없으면 Rigidbody2D의 중력을 원래대로 설정합니다.
            rigid.gravityScale = 1;
        }

        Vector2 frontVector = new Vector2(rigid.position.x+curMove*0.5f,rigid.position.y);
        Debug.DrawRay(frontVector, Vector3.down*2f, new Color(0,1,0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVector, Vector3.down, 2f, LayerMask.GetMask("GroundLayer"));
        if(rayHit.collider == null)
        {
            curMove *= -1;
            CancelInvoke();
            nextThinkTerm = Random.Range(3f, 5f);
            Invoke(nameof(Think), nextThinkTerm);
        }

        Vector3 _HPbarPos =
            Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + HPHeight, 0));
        MaxHPbar.position = _HPbarPos;
        HPbar.fillAmount = (float)HP / (float)MaxHP;
    }

    void Think()
    {
        curMove=Random.Range(-1, 2);
        nextThinkTerm = Random.Range(3f, 5f);
        Invoke(nameof(Think), nextThinkTerm);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            OnDamaged(100, other.gameObject);
        }

        if (other.gameObject.CompareTag("Ground"))
        {
            // 땅과 충돌한 경우 isGrounded를 true로 설정합니다.
            if (rigid.velocity.y <= 0)
            {
                rigid.velocity = new Vector2(rigid.velocity.x, 0);

                isGrounded = true;
            }
        }

        if (other.CompareTag("Skill"))
        {
            OnDamaged(other.gameObject.GetComponent<SkillAttack>().damage, GameObject.Find("Player"));
        }
    }

    public void OnDamaged(int damage, GameObject murderer)
    {
        HP -= damage;
        if (HP <= 0) Die(murderer);
    }

    public void Die(GameObject murderer)
    {
        // 적 캐릭터가 죽었을 때 OnDeath 이벤트를 발생시킴
        OnDeath?.Invoke();
        murderer.GetComponent<PlayerState>().EXPPoint += expValue;
        Destroy(MaxHPbar.gameObject);
        Destroy(HPbar);
        Destroy(gameObject);
    }
}
