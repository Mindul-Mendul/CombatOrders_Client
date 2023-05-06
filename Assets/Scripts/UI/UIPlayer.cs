using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayer : MonoBehaviour
{
    Transform Canvas;
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
    Transform SkillQ;
    Transform SkillW;
    Transform SkillE;
    Transform SkillR;

    Transform Stat;
    TextMeshProUGUI textLevel;
    TextMeshProUGUI textAtt;
    TextMeshProUGUI textDef;
    TextMeshProUGUI textMovSpd;
    TextMeshProUGUI textAttSpd;
    TextMeshProUGUI textMoney;

    Image UIIllustration;

    void Awake()
    {
        Canvas = transform.Find("Canvas");
        Profile = Canvas.Find("Profile");

        playerLevel = GetComponent<PlayerLevel>();
        playerState = GetComponent<PlayerState>();
        playerJob = GetComponent<PlayerJob>();

        MaxHPbar = Instantiate(prfHPbar, Canvas).GetComponent<RectTransform>();
        HPbar = MaxHPbar.Find("HPbar").GetComponent<Image>();

        MaxEXPbar = Profile.Find("PlayerUI/EXPbar_bg");
        EXPbar = MaxEXPbar.transform.Find("EXPbar").GetComponent<Image>();

        Inventory = Profile.Find("Inventory");
        for(int i=0; i<10; i++) Slot[i] = Inventory.transform.GetChild(i).gameObject;

        //Stat
        Stat = Profile.transform.Find("Stat");
        textLevel = Stat.Find("TextLevel").GetComponent<TextMeshProUGUI>();
        textAtt = Stat.Find("LabelAtt/TextAtt").GetComponent<TextMeshProUGUI>();
        textDef = Stat.Find("LabelDef/TextDef").GetComponent<TextMeshProUGUI>();
        textAttSpd = Stat.Find("LabelAsp/TextAsp").GetComponent<TextMeshProUGUI>();
        textMovSpd = Stat.Find("LabelMsp/TextMsp").GetComponent<TextMeshProUGUI>();
        textMoney = Stat.Find("LabelMoney/TextMoney").GetComponent<TextMeshProUGUI>();

        UIIllustration = GameObject.Find("Canvas/Profile/Illust").GetComponent<Image>();

        UpdateBar();
        UpdateText();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateBar();
        UpdateText();
        if(Skill) UpdateSkill();
        UIIllustration.sprite = playerJob.jobObject.GetComponent<Image>().sprite;
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
    public void UpdateJob()
    {
        //Skill
        Skill = Profile.transform.Find("UISkill");

        SkillQ = Skill.Find("UISkillQ");
        SkillQ.GetComponent<Image>().sprite = playerJob.Q.SkillSprite;
        SkillQ.GetChild(0).GetComponent<Image>().sprite = SkillQ.GetComponent<Image>().sprite;

        SkillW = Skill.Find("UISkillW");
        SkillW.GetComponent<Image>().sprite = playerJob.W.SkillSprite;
        SkillW.GetChild(0).GetComponent<Image>().sprite = SkillW.GetComponent<Image>().sprite;

        SkillE = Skill.Find("UISkillE");
        SkillE.GetComponent<Image>().sprite = playerJob.E.SkillSprite;
        SkillE.GetChild(0).GetComponent<Image>().sprite = SkillE.GetComponent<Image>().sprite;

        SkillR = Skill.Find("UISkillR");
        SkillR.GetComponent<Image>().sprite = playerJob.R.SkillSprite;
        SkillR.GetChild(0).GetComponent<Image>().sprite = SkillR.GetComponent<Image>().sprite;
    }
    void UpdateSkill()
    {
        SkillController SkillQController = playerJob.Q;
        SkillQ.GetChild(0).GetComponent<Image>().fillAmount = SkillQController.Cooltime / SkillQController.CooltimeLevelTable[SkillQController.Level];
        SkillQ.GetChild(1).GetComponent<TextMeshProUGUI>().text = SkillQController.StackCount.ToString();
        SkillQ.GetChild(2).GetComponent<TextMeshProUGUI>().text = SkillQController.Level.ToString();
        SkillQ.GetChild(3).GetComponent<TextMeshProUGUI>().text = playerJob.Skillpoint.ToString();

        SkillController SkillWController = playerJob.W;
        SkillW.GetChild(0).GetComponent<Image>().fillAmount = SkillWController.Cooltime / SkillWController.SkillObj.GetComponent<Skill>().Cooltime;
        SkillW.GetChild(1).GetComponent<TextMeshProUGUI>().text = SkillWController.StackCount.ToString();
        SkillW.GetChild(2).GetComponent<TextMeshProUGUI>().text = SkillWController.Level.ToString();
        SkillW.GetChild(3).GetComponent<TextMeshProUGUI>().text = playerJob.Skillpoint.ToString();

        SkillController SkillEController = playerJob.E;
        SkillE.GetChild(0).GetComponent<Image>().fillAmount = SkillEController.Cooltime / SkillEController.SkillObj.GetComponent<Skill>().Cooltime;
        SkillE.GetChild(1).GetComponent<TextMeshProUGUI>().text = SkillEController.StackCount.ToString();
        SkillE.GetChild(2).GetComponent<TextMeshProUGUI>().text = SkillEController.Level.ToString();
        SkillE.GetChild(3).GetComponent<TextMeshProUGUI>().text = playerJob.Skillpoint.ToString();

        SkillController SkillRController = playerJob.R;
        SkillR.GetChild(0).GetComponent<Image>().fillAmount = SkillRController.Cooltime / SkillRController.SkillObj.GetComponent<Skill>().Cooltime;
        SkillR.GetChild(1).GetComponent<TextMeshProUGUI>().text = SkillRController.StackCount.ToString();
        SkillR.GetChild(2).GetComponent<TextMeshProUGUI>().text = SkillRController.Level.ToString();
        SkillR.GetChild(3).GetComponent<TextMeshProUGUI>().text = playerJob.Skillpoint.ToString();
    }
}
