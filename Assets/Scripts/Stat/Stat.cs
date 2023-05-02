public class Stat
{
    int att;
    int def;
    int maxHP;
    float attSpd;
    float movSpd;

    public int Att { get => att; set => att = value; }
    public int Def { get => def; set => def = value; }
    public int MaxHP { get => maxHP; set => maxHP = value; }
    public float AttSpd { get => attSpd; set => attSpd = value; }
    public float MovSpd { get => movSpd; set => movSpd = value; }

    public Stat()
    {
        att = 0;
        def = 0;
        maxHP = 0;
        attSpd = 0;
        movSpd = 0;
    }
}