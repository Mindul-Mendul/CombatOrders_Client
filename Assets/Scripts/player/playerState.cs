using UnityEngine;
using UnityEngine.UI;

public class PlayerState : MonoBehaviour
{
    GameObject canvas; 
    GameObject prfHPbar;
    LevelUP LevelUP;
    public float HPHeight;
    RectTransform MaxHPbar;
    Image HPbar;
    SpriteRenderer spriteRenderer;
    Animator anim;

    Job job;
    private int level = 1;
    private int expPoint = 0;
    private int team = 1;
    private int att = 1;
    private int def = 0;
    private int maxHP = 100;
    private int hp = 100;
    private float attSpd = 1;
    private float movSpd = 4;
    private float HPRecoverTerm = 2f;
    private int hpRecovery = 5;
    public Item[] backpack;

    public Job Job { get => job; set => job = value; }
    public int Level { get => level; set => level = value; }
    public int EXPPoint { get => expPoint; set => expPoint = value; }
    public int Team { get => team; set => team = value; }
    public int Att { get => att; set => att = value; }
    public int Def { get => def; set => def = value; }
    public int MaxHP { get => maxHP; set => maxHP = value; }
    public int HP { get => hp; set => hp = value; }
    public float AttSpd { get => attSpd; set => attSpd = value; }
    public float MovSpd { get => movSpd; set => movSpd = value; }
    public Item[] Backpack { get => backpack ; set => backpack = value; }

    

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        prfHPbar = transform.GetChild(0).gameObject;
        canvas = GameObject.Find("Canvas");
        MaxHPbar = Instantiate(prfHPbar, canvas.transform).GetComponent<RectTransform>();
        HPbar = MaxHPbar.transform.GetChild(0).GetComponent<Image>();
        LevelUP = transform.GetChild(2).GetComponent<LevelUP>();
        Invoke(nameof(HPRecover), HPRecoverTerm);
    }

    private void Update()
    {
        // flip Sprite
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            spriteRenderer.flipX = (Input.GetAxisRaw("Horizontal") == -1);
        }

        //Level up
        LevelUP.Levelup();

        //Debug.Log("Level:" + level + " / EXP: " + expPoint);
    }

    void FixedUpdate()
    {
        Vector3 _HPbarPos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y+HPHeight, 10));
        MaxHPbar.position = _HPbarPos;
        MaxHPbar.transform.SetAsFirstSibling();
        HPbar.fillAmount = (float)hp / (float)maxHP;
    }

    public void SetAnimBool(string name, bool value)
    {
        anim.SetBool(name, value);
    }

    public bool GetAnimBool(string name)
    {
        return anim.GetBool(name);
    }

    public void OnDamaged(Vector2 targetPos)
    {
        //Change Layer (Immortal Active)
        gameObject.layer = 7;
        hp -= 10;

        //View Alpha
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        //Reaction Force
        Invoke("OffDamaged", 2);

        //Animation
        anim.SetTrigger("doDamaged");
    }

    public void OffDamaged()
    {
        //Change Layer (Mortal Active)
        gameObject.layer = 6;
        //View Alpha
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }
    void HPRecover()
    {
        hp += hpRecovery;
        hp = Mathf.Min(hp, maxHP);
        Invoke(nameof(HPRecover), HPRecoverTerm);
    }
}

