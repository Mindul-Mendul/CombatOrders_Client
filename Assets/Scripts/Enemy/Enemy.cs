using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public GameObject DamageText;

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

    public int MaxHP;
    public int expValue;
    public int moneyValue;

    int enemySpeed = 2;
    int curMove = 0;
    float nextThinkTerm = 3f;
    float HPHeight = 0.7f;
    int HP;

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

        HP = MaxHP;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rigid.velocity = new Vector2(curMove*enemySpeed, rigid.velocity.y);

        spriteRenderer.flipX = (curMove == 1);
        anim.SetBool("isWalking", (Mathf.Abs(curMove) == 1));

        if (isGrounded)
        {
            rigid.gravityScale = 0;
        }
        else
        {
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
        MaxHPbar.transform.SetAsFirstSibling();
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
            OnDamaged(MaxHP/10, other.GetComponent<PlayerState>());
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
            Skill skill=other.gameObject.GetComponent<Skill>();
            OnDamaged(skill.Damage, skill.playerState);
        }
    }

    public void OnDamaged(int Damage, PlayerState murderer)
    {
        int totalDamage = Damage/100 * murderer.Stat.Att;
        Transform damageText = Instantiate(DamageText.transform, new Vector3(transform.position.x, transform.position.y+spriteRenderer.size.y, -5), Quaternion.identity);
        damageText.GetComponent<DamageText>().damage = totalDamage;
        Debug.Log(damageText.GetComponent<DamageText>().damage);
        HP -= totalDamage;
        if (HP <= 0) Die(murderer);
    }

    public void Die(PlayerState murderer)
    {
        // 적 캐릭터가 죽었을 때 OnDeath 이벤트를 발생시킴
        OnDeath?.Invoke();
        murderer.EXPPoint += expValue;
        murderer.Money += moneyValue;
        Destroy(MaxHPbar.gameObject);
        Destroy(HPbar);
        Destroy(gameObject);
    }
}
