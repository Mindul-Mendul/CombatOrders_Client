using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject attackBoxPrefab; // 공격 판정 박스 프리팹
    public float attackDuration; // 공격 지속 시간
    public float attackDelay; // 공격 딜레이 시간
    private bool isAttacking = false; // 공격 중인지 여부

    private void Awake()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isAttacking)
        {
            StartCoroutine(AttackCoroutine());
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
        GameObject attackBox = Instantiate(attackBoxPrefab, attackBoxPosition, new Quaternion(0,0,0,0));

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