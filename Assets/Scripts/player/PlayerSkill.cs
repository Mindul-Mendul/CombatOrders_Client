using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkill : MonoBehaviour
{
    PlayerState playerState;
    PlayerJob playerJob;
    Skill Q;
    Skill W;
    Skill E;
    Skill R;

    public GameObject attackBoxPrefab; // 공격 판정 박스 프리팹
    public float attackDuration; // 공격 지속 시간
    public float attackDelay; // 공격 딜레이 시간
    private bool isAttacking = false; // 공격 중인지 여부
    void Awake()
    {
        playerState = GetComponent<PlayerState>();
        playerJob = GetComponent<PlayerJob>();
        Q = playerJob.Q;
        W = playerJob.W;
        E = playerJob.E;
        R = playerJob.R;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isAttacking)
        {
            StartCoroutine(AttackCoroutine());
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Q.UseSkill();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            W.UseSkill();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            E.UseSkill();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            R.UseSkill();
        }
    }

    IEnumerator AttackCoroutine()
    {
        isAttacking = true;

        // 공격 딜레이
        yield return new WaitForSeconds(attackDelay);

        // 공격 판정 박스 생성
        bool flipX = gameObject.GetComponent<SpriteRenderer>().flipX;
        Vector3 attackBoxPosition = new Vector3((flipX == true ? -1 : 1) * attackBoxPrefab.transform.position.x, attackBoxPrefab.transform.position.y, 0) + gameObject.transform.position;
        GameObject attackBox = Instantiate(attackBoxPrefab, attackBoxPosition, new Quaternion(0, 0, 0, 0));

        SpriteRenderer attackSpriteRenderer = attackBox.transform.GetChild(0).GetComponent<SpriteRenderer>();
        Collider2D attackCollider2D = attackBox.transform.GetChild(0).GetComponent<Collider2D>();

        attackSpriteRenderer.flipX = flipX;
        attackCollider2D.transform.localScale = new Vector3((flipX == true ? -1 : 1), 1, 1);

        // 공격 지속 시간
        yield return new WaitForSeconds(attackDuration);

        // 공격 판정 박스 제거
        Destroy(attackBox);

        isAttacking = false;
    }
}
