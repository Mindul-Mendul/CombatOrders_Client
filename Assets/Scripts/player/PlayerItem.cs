using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItem : MonoBehaviour
{
    bool isShop;
    void Awake()
    {
        isShop = false;
    }

    private void Update()
    {
        if (isShop)
        {
            //Debug.Log("You can use the Shop");
        }
        else
        {
            //Debug.Log("You cannot use the Shop");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Shop"))
        {
            isShop = true;
        }
        
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Shop"))
        {
            isShop = false;
        }
    }
}
