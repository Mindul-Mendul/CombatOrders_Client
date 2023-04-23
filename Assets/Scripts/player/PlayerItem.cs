using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItem : MonoBehaviour
{
    bool isShop;
    bool enableShopPanel;
    public GameObject shopPanel;
    
    void Awake()
    {
        isShop = false;
        enableShopPanel = false;
        shopPanel.SetActive(false);
    }

    private void Update()
    {
        if (isShop)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                enableShopPanel = !enableShopPanel;
            }
        }
        else
        {
            enableShopPanel = false;
        }

        shopPanel.SetActive(enableShopPanel);
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
