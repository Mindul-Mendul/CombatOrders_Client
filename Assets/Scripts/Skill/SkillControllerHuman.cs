public class SkillControllerHuman : SkillController
{
    SkillControllerHuman()
    {
        DamageLevelTable = new int[] { 0, 0, 0, 0, 0 };
        CooltimeLevelTable = new float[] { 9999f, 9999f, 9999f, 9999f, 9999f };
    }
}