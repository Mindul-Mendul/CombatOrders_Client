using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PlayerJob : MonoBehaviour
{
    GameObject job;

    PlayerState playerState;

    private int level;
    private int att;
    private int def;
    private int maxHP;
    private float movSpd;
    private float attSpd;

    public int Level { get => level; set => level = value; }
    public int Att { get => att; }
    public int Def { get => def; }
    public int MaxHP { get => maxHP; }
    public float MovSpd { get => movSpd; }
    public float AttSpd { get => attSpd; }

    SkillController FlatHit;
    SkillController Q;
    SkillController W;
    SkillController E;
    SkillController R;

    void Awake()
    {
        playerState = GetComponent<PlayerState>();
        job = GameObject.Find("Job/JobHuman");
        GetaJob(job);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && FlatHit.isCool)
        {
            FlatHit.UseSKill(gameObject);
        }
        if (Input.GetKeyDown(KeyCode.Q) && Q.isCool)
        {
            Q.UseSKill(gameObject);
        }
        if (Input.GetKeyDown(KeyCode.W) && W.isCool)
        {
            W.UseSKill(gameObject);
        }
        if (Input.GetKeyDown(KeyCode.E) && E.isCool)
        {
            E.UseSKill(gameObject);
        }
        if (Input.GetKeyDown(KeyCode.R) && R.isCool)
        {
            R.UseSKill(gameObject);
        }
    }

    public void GetaJob(GameObject jobPrefab)
    {
        GameObject jobObj = Instantiate(jobPrefab, gameObject.transform.position, new Quaternion(0, 0, 0, 0));
        Job job = jobObj.GetComponent<Job>();

        att = job.Att;
        def = job.Def;
        maxHP = job.MaxHP;
        movSpd = job.MovSpd;
        attSpd = job.AttSpd;
        
        FlatHit = job.FlatHit;
        Q = job.Q.GetComponent<SkillController>();
        W = job.W.GetComponent<SkillController>();
        E = job.E.GetComponent<SkillController>();
        R = job.R.GetComponent<SkillController>();

        playerState.Stat();
    }
}
