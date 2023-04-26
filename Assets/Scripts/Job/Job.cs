using UnityEditor;
using UnityEngine;

public class Job: MonoBehaviour
{
    int att;
    int def;
    int maxHP;
    float movSpd;
    float attSpd;

    SkillController flatHit;
    SkillController q;
    SkillController w;
    SkillController e;
    SkillController r;

    public int Att { get => att; }
    public int Def { get => def; }
    public int MaxHP { get => maxHP; }
    public float MovSpd { get => movSpd; }
    public float AttSpd { get => attSpd; }

    public SkillController FlatHit { get => flatHit; }
    public SkillController Q { get => q; }
    public SkillController W { get => w; }
    public SkillController E { get => e; }
    public SkillController R { get => r; }

    private void Awake()
    {
        flatHit = transform.GetChild(0).GetComponent<SkillController>();
        q = transform.GetChild(1).GetComponent<SkillController>();
        w = transform.GetChild(2).GetComponent<SkillController>();
        e = transform.GetChild(3).GetComponent<SkillController>();
        r = transform.GetChild(4).GetComponent<SkillController>();
    }
}