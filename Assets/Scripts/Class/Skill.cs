using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public SpriteMask spriteMask;
    public Image img_Skill;
    public int level;
    public int maxStack;
    public int stackCount;
    public float damage;
    public float cooltime;
    public bool isCooling;

    public void UseSkill()
    {
        StartCoroutine(SetCooltime(cooltime));
    }

    IEnumerator SetCooltime(float cooltime)
    {
        isCooling = true;
        float maxCool = cooltime;

        while (cooltime > 0f)
        {
            cooltime -= Time.deltaTime;
            img_Skill.fillAmount = (maxCool - cooltime) / maxCool;
            yield return new WaitForFixedUpdate();
        }

        isCooling = false;
    }
}