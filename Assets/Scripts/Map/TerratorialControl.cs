using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerratorialControl : MonoBehaviour
{
    StageManage StageManager;
    public char Terratory;
    Collider2D Collider;

    bool isShovingMatch; // 대치상황 체크
    public float ActivationPoint;
    
    int redPeople;
    int bluePeople;

    float ControlWeight = 1.62f; // 밸런스 조정을 위한 수치
    float ActivationWeight = 10.0f; // 밸런스 조정을 위한 수치

    void Awake()
    {
        StageManager = GameObject.Find("StageManager").GetComponent<StageManage>();
        Collider = gameObject.GetComponent<Collider2D>();
        isShovingMatch = false;
        ActivationPoint = 0;
        redPeople = 0;
        bluePeople = 0;
    }

    private void Update()
    { 
        //AP 계산
        CalculateActivationPoint();

        //CP 계산
        //대치상황이 아닐 때만 계산
        

        redPeople = 0;
        bluePeople = 0;
        // PlayerState asdf = collision.gameObject.GetComponent<PlayerState>();
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, Collider.bounds.size, 0f);

        foreach (Collider2D collider in colliders)
        {

            if (collider.CompareTag("Player"))
            {
                int team = collider.GetComponent<PlayerState>().Team;
                if(team == 1)
                {
                    redPeople++;
                }
                else if(team == 2)
                {
                    bluePeople++;
                }
            }
        }

        if (!isShovingMatch)
        {
            CalculateControlPoint();
        }
        //Debug.Log(Terratory + " red Character Count: " + redPeople + "/ blue Character Count: " + bluePeople);
    }

    // 점령 포인트 계산을 위한 메소드
    void CalculateControlPoint()
    {
        //레드가 점령한 상황
        if (ActivationPoint >= 100.0f)
        {
            StageManager.CPRed += (float)redPeople * ControlWeight * Time.deltaTime;
        }

        //블루가 점령한 상황
        else if (ActivationPoint <= -100.0f)
        {
            StageManager.CPBlue += (float)bluePeople * ControlWeight * Time.deltaTime;
        }
    }

    // 활성화 포인트 계산을 위한 메소드
    void CalculateActivationPoint()
    {
        // 블루가 점령 중인 상황
        if (redPeople == 0)
        {
            if(bluePeople > 0 && ActivationPoint >- 100.0f)
            {
                ActivationPoint -= (float)bluePeople * ActivationWeight * Time.deltaTime;
            }

            if(ActivationPoint < -100.0f)
            {
                ActivationPoint = -100.0f;
            }
        }

        // 레드가 점령 중인 상황
        else if (bluePeople == 0)
        {
            if (redPeople > 0 && ActivationPoint < 100.0f)
            {
                ActivationPoint += (float)redPeople * ActivationWeight * Time.deltaTime;
            }

            if (ActivationPoint > 100.0f)
            {
                ActivationPoint = 100.0f;
            }
        }
    }
}
