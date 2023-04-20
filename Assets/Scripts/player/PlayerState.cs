using UnityEngine;

public class PlayerState : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    
    PlayerLevel playerLevel;
    PlayerJob playerJob;

    private int level = 1;
    private int expPoint = 0;
    private int team = 1;
    private int att = 1;
    private int def = 0;
    private int maxHP = 100;
    private int hp = 100;
    private float attSpd = 1;
    private float movSpd = 4;

    private float HPRecoverTerm = 10f;
    private int hpRecovery = 5;
    public Item[] backpack;

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

        playerLevel = GetComponent<PlayerLevel>();
        playerJob = GetComponent<PlayerJob>();

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
        playerLevel.Levelup();
    }

    void FixedUpdate()
    {
        
    }

    public void SetAnimBool(string name, bool value)
    {
        GetComponent<Animator>().SetBool(name, value);
    }

    public bool GetAnimBool(string name)
    {
        return GetComponent<Animator>().GetBool(name);
    }

    public void OnDamaged()
    {
        //Change Layer (Immortal Active)
        gameObject.layer = 7;
        hp -= 10;

        //View Alpha
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        //Reaction Force
        Invoke(nameof(OffDamaged), 2);

        //Animation
        GetComponent<Animator>().SetTrigger("doDamaged");
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