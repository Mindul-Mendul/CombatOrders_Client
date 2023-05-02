using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class SkillController: MonoBehaviour
{
    public Image UIImage;
    public TextMeshProUGUI UIStack;
    Image UIImageCooltime;
    public Sprite UISprite;
    public GameObject SkillObj;
    public int level;
    public int maxStack = 1;
    public int stackCount;
    public float cooltime;
    bool isCooling;

    void Awake()
    {
        stackCount = maxStack;
        isCooling = false;
        if (UISprite && UIImage)
        {
            UIImage.sprite = UISprite;
            UIImageCooltime = UIImage.transform.GetChild(0).GetComponent<Image>();
            UIImageCooltime.sprite = UISprite;
            UIStack = UIImage.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            UIStack.text = stackCount.ToString();

        }
    }

    void FixedUpdate()
    {
        if (stackCount < maxStack && !isCooling)
        {
            StartCoroutine(SetCooltime());
        }
        if (UIStack)
        {
            UIStack.text = stackCount.ToString();
        }
    }

    public void UseSKill(GameObject player)
    {
        if (stackCount>0)
        {
            Skill skillObj=Instantiate(SkillObj).GetComponent<Skill>();
            skillObj.playerState = player.GetComponent<PlayerState>();
            StartCoroutine(skillObj.GetComponent<Skill>().UseSkill(player));
            stackCount--;
        }

    }
    public IEnumerator SetCooltime()
    {
        isCooling = true;
        float Cool = 0;
        while (Cool <= cooltime)
        {
            Cool += Time.deltaTime;
            if(UIImageCooltime) UIImageCooltime.fillAmount = Cool / cooltime;
            yield return new WaitForFixedUpdate();
        }
        stackCount++;
        isCooling = false;
    }
}