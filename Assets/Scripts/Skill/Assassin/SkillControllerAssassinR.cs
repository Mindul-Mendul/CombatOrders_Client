public class SkillControllerAssassinR: SkillController
{
    SkillControllerAssassinR()
    {
        DamageLevelTable = new int[] { 0, 1, 3, 5, 7 };
        CooltimeLevelTable = new float[] { 9999f, 7.0f, 5.0f, 3.0f, 1.0f };
    }
}