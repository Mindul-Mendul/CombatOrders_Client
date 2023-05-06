using System.Collections;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public Transform player;
    SpriteRenderer spriteRenderer;
    public int Damage;
    public float Cooltime;

    public PlayerState playerState;
    public float attackDuration; // 공격 지속 시간
    public float attackDelay; // 공격 딜레이 시간

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    public virtual IEnumerator UseSkill()
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
