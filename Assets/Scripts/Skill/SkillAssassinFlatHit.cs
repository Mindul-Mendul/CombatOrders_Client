using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAssassinFlatHit : Skill
{
    SkillAssassinFlatHit()
    {
        DamageLevelTable = new int[]{ 0, 100, 200, 300, 400 };
        CooltimeLevelTable = new float[] { 9999f, 7.0f, 5.0f, 3.0f, 1.0f };
    }
}
