using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopManagerScript : MonoBehaviour
{
    public int[,] shopItems = new int[13, 13];
    public string[,] shopItemsResource = new string[13, 13];
    public float silvers;
    public float golds;

    public TextMeshProUGUI coinsSilversTxt;
	//public TextMeshProUGUI coinsGoldsTxt;


    void Start()
    {
        coinsSilversTxt.text = "Coins: " + silvers.ToString();
        //coinsGoldsTxt.text = "Coins: " + golds.ToString();

        // Ids of Items
        shopItems[1, 1] = 1;
        shopItems[1, 2] = 2;
        shopItems[1, 3] = 3;
        shopItems[1, 4] = 4;
        shopItems[1, 5] = 5;
        shopItems[1, 6] = 6;
        shopItems[1, 7] = 7;
        shopItems[1, 8] = 8;
        shopItems[1, 9] = 9;
        shopItems[1, 10] = 10;
        shopItems[1, 11] = 11;
        shopItems[1, 12] = 12;

        shopItemsResource[1, 8] = "Axe";

        // Price
        shopItems[2, 1] = 10;
        shopItems[2, 2] = 20;
        shopItems[2, 3] = 30;
        shopItems[2, 4] = 40;
        shopItems[2, 5] = 50;
        shopItems[2, 6] = 60;
        shopItems[2, 7] = 70;
        shopItems[2, 8] = 80;
        shopItems[2, 9] = 90;
        shopItems[2, 10] = 100;
        shopItems[2, 11] = 110;
        shopItems[2, 12] = 120;

    }
    void Update()
    {
        
    }

    public void Buy()
    {
        GameObject ButtonRef = GameObject.FindGameObjectWithTag("ItemsShop").GetComponent<EventSystem>().currentSelectedGameObject;
        if (silvers >= shopItems[2, ButtonRef.GetComponent<ButtonInfo>().ItemID])
        {
            silvers -= shopItems[2, ButtonRef.GetComponent<ButtonInfo>().ItemID];
            coinsSilversTxt.text = "Coins: " + silvers.ToString();
            BuyItemShop(shopItemsResource[1, 8]);
        }
    }
    public void AddCoins(int amount)
    {
        silvers += amount;
        coinsSilversTxt.text = "Coins: " + silvers.ToString();
    }

    void BuyItemShop(string ItemName)
    {
        // add item into inventory
        InventorySystem.instance.AddToInventory(ItemName);
        //refrsh list
        StartCoroutine(calculate());
    }

    public IEnumerator calculate()
    {
        yield return 0;
        InventorySystem.instance.ReCalculateList();
        CraftingSystem.Instance.RefreshNeededItems();
    }
}
