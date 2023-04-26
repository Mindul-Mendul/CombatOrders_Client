using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevel : MonoBehaviour
{
    public ParticleSystem ps;
    PlayerState playerState;
    PlayerJob playerJob;

    readonly int[] levelupTable = new int[] { 0, 10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 110, 120, 99999999 };
    public int[] LevelupTable { get => levelupTable; }

    private void Awake()
    {
        playerState = transform.GetComponent<PlayerState>();
        playerJob = GetComponent<PlayerJob>();
        ps.Stop();
    }

    public void Levelup()
    {
        //Level up
        while (playerState.Level < 13 && playerState.EXPPoint >= levelupTable[playerState.Level])
        {
            playerState.EXPPoint -= levelupTable[playerState.Level++];
            playerState.HP = playerState.MaxHP;
            Play();
        }
        if (playerState.Level == 13)
        {
            playerState.EXPPoint = 0;
        }

        playerJob.Level += playerState.Level;
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
