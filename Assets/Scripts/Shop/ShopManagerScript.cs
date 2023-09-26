using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopManagerScript : MonoBehaviour
{
    public int[,] shopItems = new int[6, 6];
    public float silvers;
    public float golds;

    public TextMeshProUGUI coinsSilversTxt;
	public TextMeshProUGUI coinsGoldsTxt;


    void Start()
    {
        coinsSilversTxt.text = "Coins: " + silvers.ToString();
        coinsGoldsTxt.text = "Coins: " + golds.ToString();

        // Ids of Items
        shopItems[1, 1] = 1;
        shopItems[1, 2] = 2;
        shopItems[1, 3] = 3;
        shopItems[1, 4] = 4;
        shopItems[1, 5] = 5;

        // Price
        shopItems[2, 1] = 10;
        shopItems[2, 2] = 20;
        shopItems[2, 3] = 30;
        shopItems[2, 4] = 40;
        shopItems[2, 5] = 50;

        // Quantity
        shopItems[3, 1] = 0;
        shopItems[3, 2] = 0;
        shopItems[3, 3] = 0;
        shopItems[3, 4] = 0;
        shopItems[3, 5] = 0;

    }

    public void Buy()
    {
        GameObject ButtonRef = GameObject.FindGameObjectWithTag("ItemsShop").GetComponent<EventSystem>().currentSelectedGameObject;


        if (silvers >= shopItems[2, ButtonRef.GetComponent<ButtonInfo>().ItemID])
        {
            silvers -= shopItems[2, ButtonRef.GetComponent<ButtonInfo>().ItemID];
            shopItems[3, ButtonRef.GetComponent<ButtonInfo>().ItemID]++;
            coinsSilversTxt.text = "Coins: " + silvers.ToString();
            ButtonRef.GetComponent<ButtonInfo>().QuantityTxt.text = shopItems[3, ButtonRef.GetComponent<ButtonInfo>().ItemID].ToString();
        }
    }
}
