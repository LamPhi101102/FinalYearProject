using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

public class InteractableObject : MonoBehaviour
{
    public static InteractableObject Instance { get; set; }

    private Animator animator;

    public GameObject chestLocked;
    public GameObject chestOpen;
    public bool isOpenChest;
    public ShopManagerScript shopManager;
    public float delay = 3.0f;

    public bool playerInRange;
    public string ItemName;
    public string GetItemName()
    {
        return ItemName;
    }
    void Start()
    {
        isOpenChest = false;
    }


    void Update()
    {
        // Get the Keycode F
        if (Input.GetKeyDown(KeyCode.F) && playerInRange && SelectionManager.Instance.onTarget && SelectionManager.Instance.selectedObject == gameObject)
        {
            // check if the inventory is not full
            if (!gameObject.CompareTag("CommonChest") && !gameObject.CompareTag("ExquisiteChest") && !gameObject.CompareTag("PreciousChest") && !InventorySystem.instance.CheckIfFull())
            {
                InventorySystem.instance.AddToInventory(ItemName);
                Destroy(gameObject);
            }
            else if (gameObject.CompareTag("CommonChest"))
            {
                isOpenChest = true;
                chestLocked.SetActive(false);
                chestOpen.SetActive(true);
                Destroy(chestOpen, 3.0f);
                Destroy(chestLocked, 3.0f);
                int coins = GenerateCoinsCommonChest();
                Debug.Log(coins);
                shopManager.AddCoins(coins);
            }
            else if (gameObject.CompareTag("ExquisiteChest"))
            {
                isOpenChest = true;
                chestLocked.SetActive(false);
                chestOpen.SetActive(true);
                Destroy(chestOpen, 3.0f);
                Destroy(chestLocked, 3.0f);
                int coins = GenerateCoinsExquisiteChest();
                Debug.Log(coins);
                shopManager.AddCoins(coins);
            }
            else if (gameObject.CompareTag("PreciousChest"))
            {
                isOpenChest = true;
                chestLocked.SetActive(false);
                chestOpen.SetActive(true);
                Destroy(chestOpen, 3.0f);
                Destroy(chestLocked, 3.0f);
                int coins = GenerateCoinsPreciousChest();
                Debug.Log(coins);
                shopManager.AddCoins(coins);
            }
            else
            {
                Debug.Log("Inventory is full");
            }          
        }
    }

    public void DeleteObjectAfterDelay(float delay)
    {
        // Use the Destroy function with the specified delay.
        Destroy(chestOpen, delay);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    public int GenerateCoinsCommonChest()
    {
        return Random.Range(5, 10);
    }
    public int GenerateCoinsExquisiteChest()
    {
        return Random.Range(50, 100);
    }
    public int GenerateCoinsPreciousChest()
    {
        return Random.Range(300, 500);
    }
}
