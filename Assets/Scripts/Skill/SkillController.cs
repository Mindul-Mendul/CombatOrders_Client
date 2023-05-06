using System.Collections;
using UnityEngine;

public class SkillController: MonoBehaviour
{
    Transform player;
    public GameObject SkillObj;
    public Sprite SkillSprite;
    Skill skill;

    int level;
    int stackCount;
    float cooltime;
    public int MaxStack;
    public int Level { get => level; set => level = value; }
    public int StackCount { get => stackCount; }
    public float Cooltime { get => cooltime; set => cooltime = value; }

    public int[] DamageLevelTable;
    public float[] CooltimeLevelTable;

    public bool isFlatHit;
    bool isCooling;

    void Awake()
    {
        player = GameObject.Find("Player").transform;
        skill = SkillObj.GetComponent<Skill>();

        level = 0;
        stackCount = MaxStack;
        cooltime = 0;

        isCooling = false;
        if(isFlatHit) level++;
    }

    void FixedUpdate()
    {
        if (StackCount < MaxStack && !isCooling)
        {
            StartCoroutine(SetCooltime(skill.Cooltime));
        }
    }

    public void UseSKill()
    {
        if (StackCount > 0)
        {
            skill=Instantiate(SkillObj).GetComponent<Skill>();
            skill.player = player;
            skill.playerState = player.GetComponent<PlayerState>();
            skill.Damage = DamageLevelTable[level];
            skill.Cooltime = CooltimeLevelTable[level];

            StartCoroutine(skill.UseSkill());
            stackCount--;
        }
    }
    public IEnumerator SetCooltime(float MaxCooltime)
    {
        isCooling = true;
        cooltime = 0;
        while (cooltime <= MaxCooltime)
        {
            cooltime += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        stackCount++;
        isCooling = false;
    }
}