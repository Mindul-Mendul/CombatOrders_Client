using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManage : MonoBehaviour
{
    //TC: Terratorial Control
    //AP: Activation Point
    float cpRed;
    float cpBlue;
    float ControlWeight = 1.62f; // �뷱�� ������ ���� ��ġ

    public TerratorialControl TCA;
    public TerratorialControl TCB;
    public TerratorialControl TCC;

    public float CPRed { get => cpRed; set => cpRed = value; }
    public float CPBlue { get => cpBlue; set => cpBlue = value; }

    private void Awake()
    {
        cpRed = 0;
        cpBlue = 0;
    }

    private void Update()
    {
        
        //���� �¸� ����
        if (cpRed >= 100.0f)
        {
            WintheGame("Red");
        }
        else if (cpBlue >= 100.0f)
        {
            WintheGame("Blue");
        }

        CalculateControlPoint(TCA);
        CalculateControlPoint(TCB);
        CalculateControlPoint(TCC);
    }

    void CalculateControlPoint(TerratorialControl tc)
    {
        //���尡 ������ ��Ȳ
        if (tc.ActivationPoint >= 100.0f)
        {
            CPRed += ControlWeight * Time.deltaTime;
        }

        //��簡 ������ ��Ȳ
        else if (tc.ActivationPoint <= -100.0f)
        {
            CPBlue += ControlWeight * Time.deltaTime;
        }
    }

    // �¸� �� �����ϴ� �Լ�
    void WintheGame(string team)
    {
        cpRed = 0;
        cpBlue = 0;
        Debug.Log(team);
    }
}
