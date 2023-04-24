using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    Button buyButton;
    public Item item;
    GameObject player;
    PlayerState playerState;
    PlayerItem playerItem;
    // Start is called before the first frame update
    void Awake()
    {
        buyButton = GetComponent<Button>();
        player = GameObject.Find("Player");
        playerState = player.GetComponent<PlayerState>();
        playerItem = player.GetComponent<PlayerItem>();
        buyButton.onClick.AddListener(Buy);
    }

    void Buy()
    {
        if (playerState.Money >= item.Money)
        {
            if(playerItem.Push(item)) playerState.Money -= item.Money;
        }
    }
}
