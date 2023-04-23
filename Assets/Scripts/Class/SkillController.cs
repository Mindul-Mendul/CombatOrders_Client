using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SkillController: MonoBehaviour
{
    public Image UIImage;
    Image UIImageCooltime;
    public Sprite UISprite;
    public GameObject SkillObj;
    public int level;
    public int maxStack;
    public int stackCount;
    public float cooltime;
    public bool isCool;

    void Awake()
    {
        isCool = true;
        if (UISprite && UIImage)
        {
            UIImage.sprite = UISprite;
            UIImageCooltime = UIImage.transform.GetChild(0).GetComponent<Image>();
            UIImageCooltime.sprite = UISprite;
        }
    }

    public void UseSKill(GameObject player)
    {
        if (isCool)
        {
            StartCoroutine(Instantiate(SkillObj).GetComponent<Skill>().UseSkill(player));
            StartCoroutine(SetCooltime());
        }

    }
    public IEnumerator SetCooltime()
    {
        isCool = false;
        float Cool = 0;
        while (Cool <= cooltime)
        {
            Cool += Time.deltaTime;
            if(UIImageCooltime) UIImageCooltime.fillAmount = Cool / cooltime;
            yield return new WaitForFixedUpdate();
        }
        isCool = true;
    }
}