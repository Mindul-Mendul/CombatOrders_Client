using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClickNPC : MonoBehaviour
{
    RaycastHit2D hit;
    GameObject manager;
    WaitingManage manage;

    private void Awake()
    {
        manager = GameObject.Find("ManagerWait");
        manage = manager.GetComponent<WaitingManage>();
    }

    private void Update()
    {
        if (!manage.ExistsPopup)
        {
            MouseClickDown();
        }
    }
    void MouseClickDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            if (hit.collider)
            {
                if (hit.collider.name == this.name)
                {
                    manage.ExistsPopup = true;
                }
            }
        }
    }
}
