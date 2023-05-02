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
            playerState.HP = playerState.Stat.MaxHP;
            ps.Play();
            Invoke(nameof(Stop), 3f);
        }
        if (playerState.Level == 13)
        {
            playerState.EXPPoint = 0;
        }

        playerJob.Level += playerState.Level;
    }
    void Stop()
    {
        ps.Stop();
    }
}
