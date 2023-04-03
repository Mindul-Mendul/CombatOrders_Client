using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject attackBoxPrefab; // ���� ���� �ڽ� ������
    public float attackDuration; // ���� ���� �ð�
    public float attackDelay; // ���� ������ �ð�
    private bool isAttacking = false; // ���� ������ ����

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

        // ���� ������
        yield return new WaitForSeconds(attackDelay);

        // ���� ���� �ڽ� ����
        bool flipX = gameObject.GetComponent<SpriteRenderer>().flipX;
        Vector3 attackBoxPosition = new Vector3((flipX == true ? -1 : 1) * attackBoxPrefab.transform.position.x, attackBoxPrefab.transform.position.y, 0) + gameObject.transform.position;
        GameObject attackBox = Instantiate(attackBoxPrefab, attackBoxPosition, new Quaternion(0,0,0,0));

        SpriteRenderer attackSpriteRenderer = attackBox.transform.GetChild(0).GetComponent<SpriteRenderer>();
        Collider2D attackCollider2D = attackBox.transform.GetChild(0).GetComponent<Collider2D>();
        
        attackSpriteRenderer.flipX = flipX;
        attackCollider2D.transform.localScale = new Vector3((flipX == true ? -1 : 1), 1, 1);

        // ���� ���� �ð�
        yield return new WaitForSeconds(attackDuration);

        // ���� ���� �ڽ� ����
        Destroy(attackBox);

        isAttacking = false;
    }
}