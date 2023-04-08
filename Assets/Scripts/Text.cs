using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Text : MonoBehaviour
{
    public TextMeshProUGUI APAtext;
    public TextMeshProUGUI APBtext;
    public TextMeshProUGUI APCtext;
    public TextMeshProUGUI CPRedtext;
    public TextMeshProUGUI CPBluetext;
    public GameObject TerratoryA;
    public GameObject TerratoryB;
    public GameObject TerratoryC;

    StageManage cp;
    TerratorialControl apA;
    TerratorialControl apB;
    TerratorialControl apC;

    void Awake()
    {
        apA = TerratoryA.GetComponent<TerratorialControl>();
        apB = TerratoryB.GetComponent<TerratorialControl>();
        apC = TerratoryC.GetComponent<TerratorialControl>();
        cp = gameObject.GetComponent<StageManage>();

        APAtext.text = apA.ActivationPoint.ToString();
        APBtext.text = apB.ActivationPoint.ToString();
        APCtext.text = apC.ActivationPoint.ToString();
        CPRedtext.text = cp.CPRed.ToString();
        CPBluetext.text = cp.CPBlue.ToString();
    }

    private void Update()
    {
        APAtext.text = apA.ActivationPoint.ToString();
        APBtext.text = apB.ActivationPoint.ToString();
        APCtext.text = apC.ActivationPoint.ToString();
        CPRedtext.text = cp.CPRed.ToString();
        CPBluetext.text = cp.CPBlue.ToString();
    }
}