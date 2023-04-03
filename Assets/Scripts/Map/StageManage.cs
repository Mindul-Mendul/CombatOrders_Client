using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManage : MonoBehaviour
{
    //TC: Terratorial Control
    //AP: Activation Point
    float cpRed;
    float cpBlue;

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
    }
    
    // �¸� �� �����ϴ� �Լ�
    void WintheGame(string team)
    {
        cpRed = 0;
        cpBlue = 0;
        Debug.Log(team);
    }
}
