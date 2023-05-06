using UnityEngine;

public class PlayerJob : MonoBehaviour
{
    public GameObject jobObject;
    Job job;

    Stat stat = new();
    public Stat Stat { get => stat; }
    int skillpoint;
    public int Skillpoint { get => skillpoint; set => skillpoint = value; }

    SkillController flatHit;
    SkillController q;
    SkillController w;
    SkillController e;
    SkillController r;

    public SkillController FlatHit { get => flatHit; }
    public SkillController Q { get => q; }
    public SkillController W { get => w; }
    public SkillController E { get => e; }
    public SkillController R { get => r; }

    void Awake()
    {
        GetaJob(Resources.Load<GameObject>("Job/JobHuman"));
        job = jobObject.GetComponent<Job>();

        skillpoint = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && FlatHit.StackCount > 0) FlatHit.UseSKill();
        GetKeySkill(q, KeyCode.Q);
        GetKeySkill(w, KeyCode.W);
        GetKeySkill(e, KeyCode.E);
        GetKeySkill(r, KeyCode.R);
    }

    void GetKeySkill(SkillController sc, KeyCode key)
    {
        if (Input.GetKeyDown(key))
        {
            if (Input.GetKey(KeyCode.LeftControl) && skillpoint > 0 && sc.Level < 1)
            {
                sc.Level++;
                sc.Cooltime = sc.CooltimeLevelTable[sc.Level];
                skillpoint--;
            }
            else if (sc.Level > 0 && sc.StackCount > 0)
            {
                sc.UseSKill();
            }
        }
    }

    public void GetaJob(GameObject jobPrefab)
    {
        if(jobObject) Destroy(jobObject);
        jobObject = Instantiate(jobPrefab);
        jobObject.transform.SetParent(gameObject.transform, false);

        job = jobObject.GetComponent<Job>();
        flatHit = job.FlatHit;
        q = job.Q;
        w = job.W;
        e = job.E;
        r = job.R;

        GetComponent<PlayerState>().UpdateStat();
        GetComponent<UIPlayer>().UpdateJob();
        Levelup();
    }

    public void Levelup()
    {
        skillpoint++;

        PlayerState playerState=GetComponent<PlayerState>();
        stat.Att = job.AttLevelTable[playerState.Level];
        stat.Def = job.DefLevelTable[playerState.Level];
        stat.MaxHP = job.MaxHPLevelTable[playerState.Level];
        playerState.HP = stat.MaxHP;
        stat.MovSpd = job.MovSpdLevelTable[playerState.Level];
        stat.AttSpd = job.AttSpdLevelTable[playerState.Level];
    }
}
