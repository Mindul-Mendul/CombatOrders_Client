using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAssassinQ : Skill
{
    SkillAssassinQ()
    {
        DamageLevelTable = new int[] { 0, 1, 3, 5, 7 };
        CooltimeLevelTable = new float[] { 9999f, 7.0f, 5.0f, 3.0f, 1.0f };
    }
    public override IEnumerator UseSkill(GameObject player)
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