using UnityEditor;
using UnityEngine;

public class Job: MonoBehaviour
{
    public int[] AttLevelTable;
    public int[] DefLevelTable;
    public int[] MaxHPLevelTable;
    public float[] AttSpdLevelTable;
    public float[] MovSpdLevelTable;

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

    private void Awake()
    {
        flatHit = transform.GetChild(0).GetComponent<SkillController>();
        q = transform.GetChild(1).GetComponent<SkillController>();
        w = transform.GetChild(2).GetComponent<SkillController>();
        e = transform.GetChild(3).GetComponent<SkillController>();
        r = transform.GetChild(4).GetComponent<SkillController>();
    }
}