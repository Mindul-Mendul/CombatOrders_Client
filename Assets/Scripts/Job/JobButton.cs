using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JobButton : MonoBehaviour
{
    Button chooseButton;
    public GameObject jobPrefab;
    HiredButton hiredButton;
    Image JobIllustration;

    void Awake()
    {
        chooseButton = GetComponent<Button>();
        hiredButton = transform.parent.parent.Find("HiredBtn").GetComponent<HiredButton>();
        JobIllustration = transform.parent.parent.Find("JobIllustration").GetComponent<Image>();
        chooseButton.onClick.AddListener(ChooseJob);
    }

    void ChooseJob()
    {
        hiredButton.jobPrefab = jobPrefab;
        JobIllustration.sprite = GetComponent<Image>().sprite;
    }
}
