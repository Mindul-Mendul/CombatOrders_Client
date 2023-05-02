using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Profiling;
using UnityEngine.UI;

public class PlayerJob : MonoBehaviour
{
    public GameObject jobObject;
    Job job;

    PlayerState playerState;

    Stat stat = new();
    public Stat Stat { get => stat; }

    SkillController FlatHit;
    SkillController Q;
    SkillController W;
    SkillController E;
    SkillController R;

    void Awake()
    {
        playerState = GetComponent<PlayerState>();
        GetaJob(GameObject.Find("Job/JobHuman"));
        job = jobObject.GetComponent<Job>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && FlatHit.stackCount>0)
        {
            FlatHit.UseSKill(gameObject);
        }
        if (Input.GetKeyDown(KeyCode.Q) && Q.stackCount > 0)
        {
            Q.UseSKill(gameObject);
        }
        if (Input.GetKeyDown(KeyCode.W) && W.stackCount > 0)
        {
            W.UseSKill(gameObject);
        }
        if (Input.GetKeyDown(KeyCode.E) && E.stackCount > 0)
        {
            E.UseSKill(gameObject);
        }
        if (Input.GetKeyDown(KeyCode.R) && R.stackCount > 0)
        {
            R.UseSKill(gameObject);
        }
    }

    public void GetaJob(GameObject jobPrefab)
    {
        if(jobObject) Destroy(jobObject);
        jobObject = Instantiate(jobPrefab, gameObject.transform.position, new Quaternion(0, 0, 0, 0));
        job = jobObject.GetComponent<Job>();

        FlatHit = job.FlatHit;
        Q = job.Q;
        W = job.W;
        E = job.E;
        R = job.R;

        playerState.UpdateStat();
        Levelup();
    }

    public void Levelup()
    {
        Debug.Log(playerState.Level);
        stat.Att = job.AttLevelTable[playerState.Level];
        stat.Def = job.DefLevelTable[playerState.Level];
        stat.MaxHP = job.MaxHPLevelTable[playerState.Level];
        stat.MovSpd = job.MovSpdLevelTable[playerState.Level];
        stat.AttSpd = job.AttSpdLevelTable[playerState.Level];
    }
}
