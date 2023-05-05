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
    int skillpoint;
    public int Skillpoint { get => skillpoint; set => skillpoint = value; }

    SkillController FlatHit;
    SkillController Q;
    SkillController W;
    SkillController E;
    SkillController R;

    void Awake()
    {
        playerState = GetComponent<PlayerState>();
        GetaJob((GameObject)Resources.Load("Job/JobHuman"));
        job = jobObject.GetComponent<Job>();

        skillpoint = 0;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && FlatHit.StackCount > 0)
        {
            FlatHit.UseSKill(gameObject);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (Input.GetKey(KeyCode.LeftControl) && skillpoint > 0 && Q.Level < 4)
            {
                 Q.Level++;
                 skillpoint--;
            }
            else if (Q.Level > 0 && Q.StackCount > 0)
            {
                 Q.UseSKill(gameObject);
            }
            
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (Input.GetKey(KeyCode.LeftControl) && skillpoint > 0 && W.Level < 4)
            {
                W.Level++;
                skillpoint--;
            }
            else if (W.Level > 0 && W.StackCount > 0)
            {
                W.UseSKill(gameObject);
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Input.GetKey(KeyCode.LeftControl) && skillpoint > 0 && E.Level < 4)
            {
                E.Level++;
                skillpoint--;
            }
            else if (E.Level > 0 && E.StackCount > 0)
            {
                E.UseSKill(gameObject);
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (Input.GetKey(KeyCode.LeftControl) && skillpoint > 0 && R.Level < 1)
            {
                R.Level++;
                skillpoint--;
            }
            else if (R.Level > 0 && R.StackCount > 0)
            {
                R.UseSKill(gameObject);
            }
        }
    }

    public void GetaJob(GameObject jobPrefab)
    {
        if(jobObject) Destroy(jobObject);
        jobObject = Instantiate(jobPrefab);
        jobObject.transform.SetParent(gameObject.transform);
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
        skillpoint++;
        stat.Att = job.AttLevelTable[playerState.Level];
        stat.Def = job.DefLevelTable[playerState.Level];
        stat.MaxHP = job.MaxHPLevelTable[playerState.Level];
        playerState.HP = stat.MaxHP;
        stat.MovSpd = job.MovSpdLevelTable[playerState.Level];
        stat.AttSpd = job.AttSpdLevelTable[playerState.Level];
    }
}
