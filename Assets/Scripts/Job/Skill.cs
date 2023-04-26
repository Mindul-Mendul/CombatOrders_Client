using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    SpriteMask spriteMask;
    public int damage;

    public float attackDuration; // 공격 지속 시간
    public float attackDelay; // 공격 딜레이 시간

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteMask = GetComponent<SpriteMask>();
    }

    // Update is called once per frame
    public IEnumerator UseSkill(GameObject player)
    {
        // 딜레이
        yield return new WaitForSeconds(attackDuration);

        // 공격 판정 박스 생성
        bool flipX = player.GetComponent<SpriteRenderer>().flipX;
        Vector3 _pos = player.transform.position;

        spriteRenderer.flipX = flipX;
        gameObject.transform.position = _pos + (flipX == true ? -1 : 1) * gameObject.transform.position;

        // 지속 시간
        yield return new WaitForSeconds(attackDelay);

        // 공격 판정 박스 제거
        Destroy(gameObject);
    }
}
