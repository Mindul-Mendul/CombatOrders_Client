using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerratorialControl : MonoBehaviour
{
    StageManage StageManager;
    public char Terratory;
    Collider2D Collider;

    bool isShovingMatch; // ��ġ��Ȳ üũ
    public float ActivationPoint;
    
    int redPeople;
    int bluePeople;

    float ActivationWeight = 10.0f; // �뷱�� ������ ���� ��ġ

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
        redPeople = 0;
        bluePeople = 0;

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

        isShovingMatch = (redPeople * bluePeople > 0);
        CalculateActivationPoint();
    }

    // ���� ����Ʈ ����� ���� �޼ҵ�
    

    // Ȱ��ȭ ����Ʈ ����� ���� �޼ҵ�
    void CalculateActivationPoint()
    {
        // ��簡 ���� ���� ��Ȳ
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

        // ���尡 ���� ���� ��Ȳ
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
