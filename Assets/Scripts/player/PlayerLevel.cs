using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevel : MonoBehaviour
{
    public ParticleSystem ps;
    PlayerState playerState;

    readonly int[] levelupTable = new int[] { 0, 10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 110, 120, 99999999 };
    public int[] LevelupTable { get => levelupTable; }

    private void Awake()
    {
        playerState = transform.GetComponent<PlayerState>();
        ps.Stop();
    }

    public void Levelup()
    {
        //Level up
        while (playerState.Level < 13 && playerState.EXPPoint >= levelupTable[playerState.Level])
        {
            playerState.EXPPoint -= levelupTable[playerState.Level++];
            Play();
        }
        if (playerState.Level == 13)
        {
            playerState.EXPPoint = 0;
        }
    }

    private void Play()
    {
        // 파티클 시스템을 재생합니다.
        ps.Play();

        // 3초 후에 Stop 메서드를 호출하여 파티클 시스템을 멈춥니다.
        Invoke(nameof(Stop), 3f);
    }

    private void Stop()
    {
        // 파티클 시스템을 멈춥니다.
        ps.Stop();
    }
}
