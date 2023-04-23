using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PlayerJob : MonoBehaviour
{
    public GameObject job;
    int Att;
    int Def;
    int MaxHP;
    float MovSpd;
    float AttSpd;

    SkillController FlatHit;
    SkillController Q;
    SkillController W;
    SkillController E;
    SkillController R;

    void Awake()
    {
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

    void GetaJob(GameObject jobPrefab)
    {
        GameObject jobObj = Instantiate(jobPrefab, gameObject.transform.position, new Quaternion(0, 0, 0, 0));
        Job job = jobObj.GetComponent<Job>();

        Att = job.Att;
        Def = job.Def;
        MaxHP = job.MaxHP;
        MovSpd = job.MovSpd;
        AttSpd = job.AttSpd;
        
        FlatHit = job.FlatHit;
        Q = job.Q.GetComponent<SkillController>();
        W = job.W.GetComponent<SkillController>();
        E = job.E.GetComponent<SkillController>();
        R = job.R.GetComponent<SkillController>();
    }
}
