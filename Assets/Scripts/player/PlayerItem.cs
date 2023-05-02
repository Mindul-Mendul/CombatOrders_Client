using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class PlayerItem : MonoBehaviour
{
    Stat stat = new();
    bool isShop;
    bool enableShopPanel;
    public GameObject shopPanel;

    public Stat Stat { get => stat; }

    UIPlayer ui;
    PlayerState playerState;
    private Item[] backpack = new Item[10];

    void Awake()
    {
        playerState = GetComponent<PlayerState>();
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

                stat.Att += backpack[i].Att;
                stat.Def += backpack[i].Def;
                stat.MaxHP += backpack[i].MaxHP;
                stat.AttSpd += backpack[i].AttSpd;
                stat.MovSpd += backpack[i].MovSpd;
                playerState.UpdateStat();

                return true;
            }
        }
        return false;
    }

    public bool Pop(int i)
    {
        stat.Att -= backpack[i].Att;
        stat.Def -= backpack[i].Def;
        stat.MaxHP -= backpack[i].MaxHP;
        stat.AttSpd -= backpack[i].AttSpd;
        stat.MovSpd -= backpack[i].MovSpd;
        playerState.UpdateStat();

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
