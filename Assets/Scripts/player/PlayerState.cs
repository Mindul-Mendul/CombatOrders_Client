using UnityEngine;
using UnityEngine.Playables;

public class PlayerState : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    PlayerMove playerMove;
    PlayerLevel playerLevel;
    PlayerJob playerJob;
    PlayerItem playerItem;

    Stat stat = new();
    private int level = 1;
    private int expPoint = 0;
    private int team = 1;
    private int hp = 1;
    private int money = 10000;
    private float hpRecoverTerm = 0.5f;
    private int hpRecovery = 5;

    public Stat Stat { get => stat; }
    public int Level { get => level; set => level = value; }
    public int EXPPoint { get => expPoint; set => expPoint = value; }
    public int Team { get => team; set => team = value; }
    public int HP { get => hp; set => hp = value; }
    private float HPRecoverTerm { get => hpRecoverTerm; set => hpRecoverTerm = value; }
    private int HPRecovery { get => hpRecovery; set => hpRecovery = value; }
    public int Money { get => money; set => money = value; }

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        playerMove = GetComponent<PlayerMove>();
        playerLevel = GetComponent<PlayerLevel>();
        playerJob = GetComponent<PlayerJob>();
        playerItem = GetComponent<PlayerItem>();

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

    public void UpdateStat()
    {
        stat.Att = playerJob.Stat.Att + playerItem.Stat.Att;
        stat.Def = playerJob.Stat.Def + playerItem.Stat.Def;
        stat.MaxHP = playerJob.Stat.MaxHP + playerItem.Stat.MaxHP;
        stat.AttSpd = playerJob.Stat.AttSpd + playerItem.Stat.AttSpd;
        stat.MovSpd = playerJob.Stat.MovSpd + playerItem.Stat.MovSpd;

        playerMove.MaxSpeed = stat.MovSpd;
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
        hp = Mathf.Min(hp, stat.MaxHP);
        Invoke(nameof(HPRecover), HPRecoverTerm);
    }
}