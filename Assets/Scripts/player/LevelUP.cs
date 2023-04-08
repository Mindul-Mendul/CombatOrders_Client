using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUP : MonoBehaviour
{
    private ParticleSystem ps;
    PlayerState p;

    readonly int[] levelupTable = new int[] { 0, 10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 110, 120 };

    private void Awake()
    {
        ps = GetComponent<ParticleSystem>();
        p = transform.parent.GetComponent<PlayerState>();
        ps.Stop();
    }

    public void Levelup()
    {
        //Level up
        while (p.Level < 13 && p.EXPPoint >= levelupTable[p.Level])
        {
            p.EXPPoint -= levelupTable[p.Level++];
            Play();
        }
        if (p.Level == 13)
        {
            p.EXPPoint = 0;
        }
    }

    private void Play()
    {
        // ��ƼŬ �ý����� ����մϴ�.
        ps.Play();

        // 3�� �Ŀ� Stop �޼��带 ȣ���Ͽ� ��ƼŬ �ý����� ����ϴ�.
        Invoke(nameof(Stop), 3f);
    }

    private void Stop()
    {
        // ��ƼŬ �ý����� ����ϴ�.
        ps.Stop();
    }
}
