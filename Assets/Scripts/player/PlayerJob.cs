using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Profiling;
using UnityEngine.UI;

public class PlayerJob : MonoBehaviour
{
    GameObject job;

    PlayerState playerState;
    Image ui;

    private int level;
    Stat stat = new();

    public int Level { get => level; set => level = value; }
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
        ui = GameObject.Find("Canvas/Profile/Illust").GetComponent<Image>();
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
        Destroy(job);
        job = Instantiate(jobPrefab, gameObject.transform.position, new Quaternion(0, 0, 0, 0));
        Job jobJob = job.GetComponent<Job>();

        stat.Att = jobJob.Att;
        stat.Def = jobJob.Def;
        stat.MaxHP = jobJob.MaxHP;
        stat.MovSpd = jobJob.MovSpd;
        stat.AttSpd = jobJob.AttSpd;
        
        FlatHit = jobJob.FlatHit;
        Q = jobJob.Q;
        W = jobJob.W;
        E = jobJob.E;
        R = jobJob.R;

        if(ui) ui.GetComponent<Image>().sprite = job.GetComponent<Image>().sprite;

        playerState.UpdateStat();
    }
}
