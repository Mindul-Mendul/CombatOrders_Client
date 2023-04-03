using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickPopup : MonoBehaviour
{
    RaycastHit2D hit;
    GameObject manager;
    WaitingManage manage;
    string MapCombat = "MapCombat";

    public bool YesNo;

    private void Awake()
    {
        manager = GameObject.Find("ManagerWait");
        manage = manager.GetComponent<WaitingManage>();
    }

    private void Update()
    {
        MouseClickDown();
    }

    void MouseClickDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            if(hit.collider.name == this.name)
            {
                // yes
                if (YesNo)
                {
                    Yes();
                }
                // no
                else
                {
                    No();
                }
            }
        }
    }

    void Yes()
    {
        manage.ExistsPopup = false;
        SceneManager.LoadScene(MapCombat);
    }

    void No()
    {
        manage.ExistsPopup = false;
    }
}