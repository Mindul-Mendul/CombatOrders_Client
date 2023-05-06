using System.Collections;
using UnityEngine;

public class SkillAssassinQ : Skill
{
    public override IEnumerator UseSkill()
    {
        yield return new WaitForSeconds(attackDuration);
        player.GetComponent<PlayerMove>().Hide = true;
        Debug.Log(player.GetComponent<PlayerMove>().Hide);
        // ���� �ð�
        yield return new WaitForSeconds(5);
        player.GetComponent<PlayerMove>().Hide = false;
        Debug.Log(player.GetComponent<PlayerMove>().Hide);
        // ���� ���� �ڽ� ����
        Destroy(gameObject);
    }
}