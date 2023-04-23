using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManage : MonoBehaviour
{
    //TC: Terratorial Control
    //AP: Activation Point
    float cpRed;
    float cpBlue;
    float ControlWeight = 1.62f; // 밸런스 조정을 위한 수치

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
        
        //게임 승리 조건
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
        //레드가 점령한 상황
        if (tc.ActivationPoint >= 100.0f)
        {
            CPRed += ControlWeight * Time.deltaTime;
        }

        //블루가 점령한 상황
        else if (tc.ActivationPoint <= -100.0f)
        {
            CPBlue += ControlWeight * Time.deltaTime;
        }
    }

    // 승리 시 정리하는 함수
    void WintheGame(string team)
    {
        cpRed = 0;
        cpBlue = 0;
        Debug.Log(team);
    }
}
