using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JobButton : MonoBehaviour
{
    Button chooseButton;
    public GameObject jobPrefab;
    HiredButton hiredButton;

    void Awake()
    {
        chooseButton = GetComponent<Button>();
        hiredButton = transform.parent.parent.Find("HiredBtn").gameObject.GetComponent<HiredButton>();
        chooseButton.onClick.AddListener(ChooseJob);
    }

    void ChooseJob()
    {
        hiredButton.jobPrefab = jobPrefab;
    }
}
