using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.UI;

public class UIPlayer : MonoBehaviour
{
    GameObject Canvas;
    GameObject Profile;
    PlayerLevel playerLevel;
    PlayerState playerState;
    PlayerJob playerJob;

    readonly float HPHeight = 1.0f;
    
    public GameObject prfHPbar;
    RectTransform MaxHPbar;
    Image HPbar;

    GameObject MaxEXPbar;
    Image EXPbar;

    public TextMeshProUGUI textLevel;
    public TextMeshProUGUI textAttack;
    public TextMeshProUGUI textDefense;
    public TextMeshProUGUI textSpeed;

    void Awake()
    {
        Canvas = GameObject.Find("Canvas");
        Profile = Canvas.transform.Find("Profile").gameObject;

        playerLevel = GetComponent<PlayerLevel>();
        playerState = GetComponent<PlayerState>();
        playerJob = GetComponent<PlayerJob>();

        MaxHPbar = Instantiate(prfHPbar, Canvas.transform).GetComponent<RectTransform>();
        HPbar = MaxHPbar.transform.Find("HPbar").GetComponent<Image>();

        MaxEXPbar = Profile.transform.Find("PlayerUI/EXPbar_bg").gameObject;
        EXPbar = MaxEXPbar.transform.Find("EXPbar").GetComponent<Image>();

        UpdateText();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 _HPbarPos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + HPHeight, 0));
        MaxHPbar.position = _HPbarPos;
        MaxHPbar.transform.SetAsFirstSibling();
        HPbar.fillAmount = (float)playerState.HP / playerState.MaxHP;
        EXPbar.fillAmount = (float)playerState.EXPPoint / playerLevel.LevelupTable[playerState.Level];

        UpdateText();
    }

    void UpdateText()
    {
        textLevel.text = playerState.Level.ToString();
        textAttack.text = playerState.Att.ToString();
        textDefense.text = playerState.Def.ToString();
        textSpeed.text = playerState.MovSpd.ToString();
    }
}
