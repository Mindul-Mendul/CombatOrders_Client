using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameObject PortalExit;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("qwer");
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                Vector2 portalPos = PortalExit.transform.position;

                col.attachedRigidbody.MovePosition(portalPos);
            }
        }
    }
}
