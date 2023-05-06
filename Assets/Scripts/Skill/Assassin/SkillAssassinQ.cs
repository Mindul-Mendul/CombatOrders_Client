using System.Collections;
using UnityEngine;

public class SkillAssassinQ : Skill
{
    public override IEnumerator UseSkill()
    {
        yield return new WaitForSeconds(attackDuration);
        player.GetComponent<PlayerMove>().Hide = true;
        Debug.Log(player.GetComponent<PlayerMove>().Hide);
        // 지속 시간
        yield return new WaitForSeconds(5);
        player.GetComponent<PlayerMove>().Hide = false;
        Debug.Log(player.GetComponent<PlayerMove>().Hide);
        // 공격 판정 박스 제거
        Destroy(gameObject);
    }
}