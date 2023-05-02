using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class SkillController: MonoBehaviour
{
    PlayerJob playerJob;

    public Image UIImage;
    public TextMeshProUGUI UIStack;
    public TextMeshProUGUI UILevel;
    public TextMeshProUGUI UISkillpoint;
    Image UIImageCooltime;
    public Sprite UISprite;
    public GameObject SkillObj;
    Skill skill;


    int level;
    public int Level { get => level; set => level = value; }
    public int MaxStack;
    public int StackCount;

    bool isCooling;

    void Awake()
    {
        playerJob = GameObject.Find("Player").GetComponent<PlayerJob>();
        skill = SkillObj.GetComponent<Skill>(); 
        
        level = 0;
        StackCount = MaxStack;

        isCooling = false;

        // 스킬인 경우
        if (UISprite && UIImage)
        {
            UIImage.sprite = UISprite;
            UIImageCooltime = UIImage.transform.GetChild(0).GetComponent<Image>();
            UIImageCooltime.sprite = UISprite;
            UIStack = UIImage.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            UIStack.text = StackCount.ToString();
            UILevel = UIImage.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
            UILevel.text = level.ToString();
            UISkillpoint = UIImage.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
            UISkillpoint.text = playerJob.Skillpoint.ToString();
        }
        // 평타인 경우
        else
        {
            level++;
        }
    }

    void FixedUpdate()
    {
        if (StackCount < MaxStack && !isCooling)
        {
            StartCoroutine(SetCooltime(skill.Cooltime));
        }
        if (UIStack && UILevel)
        {
            UIStack.text = StackCount.ToString();
            UILevel.text = level.ToString();
            UISkillpoint.text = playerJob.Skillpoint.ToString();
        }
    }

    public void UseSKill(GameObject player)
    {
        if (StackCount > 0)
        {
            skill=Instantiate(SkillObj).GetComponent<Skill>();
            skill.playerState = player.GetComponent<PlayerState>();
            skill.Damage = skill.DamageLevelTable[level];
            skill.Cooltime = skill.CooltimeLevelTable[level];

            StartCoroutine(skill.UseSkill(player));
            StackCount--;
        }
    }
    public IEnumerator SetCooltime(float cooltime)
    {
        isCooling = true;
        float Cool = 0;
        while (Cool <= cooltime)
        {
            Cool += Time.deltaTime;
            if(UIImageCooltime) UIImageCooltime.fillAmount = Cool / cooltime;
            yield return new WaitForFixedUpdate();
        }
        StackCount++;
        isCooling = false;
    }
}