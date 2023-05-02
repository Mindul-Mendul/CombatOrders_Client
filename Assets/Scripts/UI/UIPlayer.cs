using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.UI;

public class UIPlayer : MonoBehaviour
{
    GameObject Canvas;
    Transform Profile;
    PlayerLevel playerLevel;
    PlayerState playerState;
    PlayerJob playerJob;

    readonly float HPHeight = 1.0f;
    
    public GameObject prfHPbar;
    RectTransform MaxHPbar;
    Image HPbar;

    Transform MaxEXPbar;
    Image EXPbar;

    Transform Inventory;
    public GameObject[] Slot = new GameObject[10];

    Transform Skill;

    Transform Stat;
    TextMeshProUGUI textLevel;
    TextMeshProUGUI textAtt;
    TextMeshProUGUI textDef;
    TextMeshProUGUI textMovSpd;
    TextMeshProUGUI textAttSpd;
    TextMeshProUGUI textMoney;

    void Awake()
    {
        Canvas = GameObject.Find("Canvas");
        Profile = Canvas.transform.Find("Profile");

        playerLevel = GetComponent<PlayerLevel>();
        playerState = GetComponent<PlayerState>();
        playerJob = GetComponent<PlayerJob>();

        MaxHPbar = Instantiate(prfHPbar, Canvas.transform).GetComponent<RectTransform>();
        HPbar = MaxHPbar.Find("HPbar").GetComponent<Image>();

        MaxEXPbar = Profile.Find("PlayerUI/EXPbar_bg");
        EXPbar = MaxEXPbar.transform.Find("EXPbar").GetComponent<Image>();

        Inventory = Profile.Find("Inventory");
        for(int i=0; i<10; i++) Slot[i] = Inventory.transform.GetChild(i).gameObject;

        Skill = Profile.transform.Find("UISkill");

        Stat = Profile.transform.Find("Stat");
        textLevel = Stat.Find("TextLevel").GetComponent<TextMeshProUGUI>();
        textAtt = Stat.Find("LabelAtt/TextAtt").GetComponent<TextMeshProUGUI>();
        textDef = Stat.Find("LabelDef/TextDef").GetComponent<TextMeshProUGUI>();
        textAttSpd = Stat.Find("LabelAsp/TextAsp").GetComponent<TextMeshProUGUI>();
        textMovSpd = Stat.Find("LabelMsp/TextMsp").GetComponent<TextMeshProUGUI>();
        textMoney = Stat.Find("LabelMoney/TextMoney").GetComponent<TextMeshProUGUI>();

        UpdateText();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateBar();
        UpdateText();
    }

    void UpdateBar()
    {
        Vector3 _HPbarPos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + HPHeight, 0));
        MaxHPbar.position = _HPbarPos;
        MaxHPbar.transform.SetAsFirstSibling();
        HPbar.fillAmount = (float)playerState.HP / playerState.Stat.MaxHP;
        EXPbar.fillAmount = (float)playerState.EXPPoint / playerLevel.LevelupTable[playerState.Level];
    }

    void UpdateText()
    {
        textLevel.text = playerState.Level.ToString();
        textAtt.text = playerState.Stat.Att.ToString();
        textDef.text = playerState.Stat.Def.ToString();
        textMovSpd.text = playerState.Stat.MovSpd.ToString();
        textAttSpd.text = playerState.Stat.AttSpd.ToString();
        textMoney.text = playerState.Money.ToString();
    }
}
