using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HiredButton : MonoBehaviour
{
    Button hiredButton;
    public GameObject jobPrefab;
    GameObject player;
    PlayerJob playerJob;
    GameObject jobPanel;

    void Awake()
    {
        hiredButton = GetComponent<Button>();
        player = GameObject.Find("Player");
        playerJob = player.GetComponent<PlayerJob>();
        jobPanel = transform.parent.gameObject;
        hiredButton.onClick.AddListener(Hired);
    }

    void Hired()
    {
        if (jobPrefab)
        {
            playerJob.GetaJob(jobPrefab);
            jobPanel.SetActive(false);
        }
    }
}
