using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingManage : MonoBehaviour
{
    // Start is called before the first frame update
    bool existsPopup;
    public GameObject Popup;
    public bool ExistsPopup { get => existsPopup; set => existsPopup = value; }
    void Awake()
    {
        existsPopup = false;
    }

    // Update is called once per frame
    void Update()
    {
        Popup.SetActive(existsPopup);
    }
}
