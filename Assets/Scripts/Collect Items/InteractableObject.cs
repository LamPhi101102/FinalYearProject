using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class InteractableObject : MonoBehaviour
{
    private Animator animator;

    public GameObject chestLocked;
    public GameObject chestOpen;

    public float delay = 3.0f;

    public bool playerInRange;
    public string ItemName;
    public string GetItemName()
    {
        return ItemName;
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
                chestLocked.SetActive(false);
                chestOpen.SetActive(true);
                Destroy(chestOpen, 3.0f);
                Destroy(chestLocked, 3.0f);
            }
            else if (gameObject.CompareTag("ExquisiteChest"))
            {
                chestLocked.SetActive(false);
                chestOpen.SetActive(true);
                Destroy(chestOpen, 3.0f);
                Destroy(chestLocked, 3.0f);
            }
            else if (gameObject.CompareTag("PreciousChest"))
            {
                chestLocked.SetActive(false);
                chestOpen.SetActive(true);
                Destroy(chestOpen, 3.0f);
                Destroy(chestLocked, 3.0f);
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


}
