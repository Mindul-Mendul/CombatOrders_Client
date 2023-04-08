using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkill : MonoBehaviour
{
    PlayerState state;
    Job job;
    Skill Q;
    Skill W;
    Skill E;
    Skill R;
    void Awake()
    {
        state = GetComponent<PlayerState>();
        job = state.Job;
        Q = job.Q;
        W = job.W;
        E = job.E;
        R = job.R;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Q.useSkill();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            W.useSkill();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            E.useSkill();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            R.useSkill();
        }
    }
}
