using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class InteractableObject : MonoBehaviour
{

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
            if (!InventorySystem.instance.CheckIfFull())
            {

                InventorySystem.instance.AddToInventory(ItemName);
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("Inventory is full");
            }
            

        }
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
