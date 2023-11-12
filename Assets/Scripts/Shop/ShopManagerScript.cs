using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopManagerScript : MonoBehaviour
{
    public int[,] shopItems = new int[25, 25];
    public string[,] shopItemsResource = new string[25, 25];
    public float silvers;
    public float golds;
    public GameObject shopUI;
    public GameObject shopPage1;
    public GameObject shopPage2;

    public TextMeshProUGUI coinsSilversTxt;
    //public TextMeshProUGUI coinsGoldsTxt;
    Button NextPage;
    Button PreviousPage;

    void Start()
    {
        NextPage = shopUI.transform.Find("Content").transform.Find("NextPage").GetComponent<Button>();
        NextPage.onClick.AddListener(delegate { NextToPage(); });

        PreviousPage = shopUI.transform.Find("Content2").transform.Find("PreviousPage").GetComponent<Button>();
        PreviousPage.onClick.AddListener(delegate { PreviousToPage(); });

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
        shopItems[1, 13] = 13;
        shopItems[1, 14] = 14;
        shopItems[1, 15] = 15;
        shopItems[1, 16] = 16;
        shopItems[1, 17] = 17;
        shopItems[1, 18] = 18;
        shopItems[1, 19] = 19;
        shopItems[1, 20] = 20;
        shopItems[1, 21] = 21;
        shopItems[1, 22] = 22;
        shopItems[1, 23] = 23;
        shopItems[1, 24] = 24;

        // item Resource after buying
        shopItemsResource[1, 1] = "Bow";
        shopItemsResource[1, 2] = "Axe";
        shopItemsResource[1, 3] = "Sword";
        shopItemsResource[1, 4] = "Stone Armor";
        shopItemsResource[1, 5] = "Gold Ring";
        shopItemsResource[1, 6] = "Wooden Gloves";
        shopItemsResource[1, 7] = "Boots";
        shopItemsResource[1, 8] = "Stone Shoulder";
        shopItemsResource[1, 9] = "Stone Helmet";
        shopItemsResource[1, 10] = "Wooden Bracers";
        shopItemsResource[1, 11] = "Wooden Belt";
        shopItemsResource[1, 12] = "Water";
        shopItemsResource[1, 13] = "Apple";
        shopItemsResource[1, 14] = "Bread";
        shopItemsResource[1, 15] = "Big Bread";
        shopItemsResource[1, 16] = "Piece Of Cheese";
        shopItemsResource[1, 17] = "Cheese";
        shopItemsResource[1, 18] = "Orange";
        shopItemsResource[1, 19] = "Pumpkin";
        shopItemsResource[1, 20] = "Tomato";
        shopItemsResource[1, 21] = "Watermelon";
        shopItemsResource[1, 22] = "Meat";
        shopItemsResource[1, 23] = "Schnitzel";
        shopItemsResource[1, 24] = "Sausage";


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
        shopItems[2, 12] = 12;
        shopItems[2, 13] = 13;
        shopItems[2, 14] = 14;
        shopItems[2, 15] = 15;
        shopItems[2, 16] = 16;
        shopItems[2, 17] = 17;
        shopItems[2, 18] = 18;
        shopItems[2, 19] = 19;
        shopItems[2, 20] = 20;
        shopItems[2, 21] = 21;
        shopItems[2, 22] = 22;
        shopItems[2, 23] = 23;
        shopItems[2, 24] = 24;

    }

    private void NextToPage()
    {
        shopPage1.SetActive(false);
        shopPage2.SetActive(true);
    }
    private void PreviousToPage()
    {
        shopPage1.SetActive(true);
        shopPage2.SetActive(false);
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
            BuyItemShop(shopItemsResource[1, ButtonRef.GetComponent<ButtonInfo>().ItemID]);
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
