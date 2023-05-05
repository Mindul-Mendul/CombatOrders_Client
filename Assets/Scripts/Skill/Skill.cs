using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public int Damage;
    public float Cooltime;

    public int[] DamageLevelTable;
    public float[] CooltimeLevelTable;

    public PlayerState playerState;
    public float attackDuration; // ���� ���� �ð�
    public float attackDelay; // ���� ������ �ð�

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    public virtual IEnumerator UseSkill(GameObject player)
    {
        // ������
        yield return new WaitForSeconds(attackDuration);

        // ���� ���� �ڽ� ����
        bool flipX = player.GetComponent<SpriteRenderer>().flipX;
        Vector3 _pos = player.transform.position;

        spriteRenderer.flipX = flipX;
        gameObject.transform.position = _pos + (flipX == true ? -1 : 1) * gameObject.transform.position;

        // ���� �ð�
        yield return new WaitForSeconds(attackDelay);

        // ���� ���� �ڽ� ����
        Destroy(gameObject);
    }
}
