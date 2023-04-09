using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    GameObject Canvas;
    GameObject Profile;
    PlayerLevel playerLevel;
    PlayerState playerState;

    public float HPHeight;
    
    GameObject prfHPbar;
    RectTransform MaxHPbar;
    Image HPbar;

    GameObject MaxEXPbar;
    Image EXPbar;

    void Awake()
    {
        Canvas = GameObject.Find("Canvas");
        Profile = Canvas.transform.Find("Profile").gameObject;

        playerLevel = GetComponent<PlayerLevel>();
        playerState = GetComponent<PlayerState>();

        prfHPbar = transform.GetChild(1).gameObject;
        MaxHPbar = Instantiate(prfHPbar, Canvas.transform).GetComponent<RectTransform>();
        HPbar = MaxHPbar.transform.Find("HPbar").GetComponent<Image>();

        MaxEXPbar = Profile.transform.Find("PlayerUI/EXPbar_bg").gameObject;
        EXPbar = MaxEXPbar.transform.Find("EXPbar").GetComponent<Image>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 _HPbarPos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + HPHeight, 0));
        MaxHPbar.position = _HPbarPos;
        MaxHPbar.transform.SetAsFirstSibling();
        HPbar.fillAmount = (float)playerState.HP / playerState.MaxHP;
        EXPbar.fillAmount = (float)playerState.EXPPoint / playerLevel.LevelupTable[playerState.Level];
    }
}
