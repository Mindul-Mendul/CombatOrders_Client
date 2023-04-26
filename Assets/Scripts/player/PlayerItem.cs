using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerItem : MonoBehaviour
{
    bool isShop;
    bool enableShopPanel;
    public GameObject shopPanel;

    UIPlayer ui;
    private Item[] backpack = new Item[10];

    void Awake()
    {
        ui = GetComponent<UIPlayer>();
        isShop = false;
        enableShopPanel = false;
        shopPanel.SetActive(false);
    }

    public bool Push(Item item)
    {
        for(int i=0; i<10; i++)
        {
            if (!backpack[i])
            {
                backpack[i] = item;
                ui.Slot[i].GetComponent<Image>().sprite = item.image.sprite;
                return true;
            }
        }
        return false;
    }

    public bool Pop(int i)
    {
        backpack[i] = null;
        return true;
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
            enableShopPanel = false;
        }
    }
}
