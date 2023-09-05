using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipSystem : MonoBehaviour
{
    public static EquipSystem Instance { get; set; }

    public GameObject quickSlotsPanel;
    public List<GameObject> quickSlotsList = new List<GameObject>();
    public List<string> itemList = new List<string>();


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    // Start is called before the first frame update
    private void Start()
    {
        PopulateSlotList();
    }

    private void PopulateSlotList()
    {
        foreach (Transform child in quickSlotsPanel.transform)
        {
            if (child.CompareTag("QuickSlot"))
            {
                quickSlotsList.Add(child.gameObject);
            }
        }
    }
    public void AddToQuickSlots(GameObject itemToEquip)
    {
        // Find the next empty slot
        GameObject availableSlot = FindNextEmptySlot();
        // set Transform of our object
        itemToEquip.transform.SetParent(availableSlot.transform, false);
        // Getting Clean name
        string cleanName = itemToEquip.name.Replace("(Clone)","");
        itemList.Add(cleanName);

        InventorySystem.instance.ReCalculateList();
    }

    private GameObject FindNextEmptySlot()
    {
        foreach (GameObject slot in quickSlotsList)
        {
            if (slot.transform.childCount == 1)
            {
                return slot;
            }
        }
        return new GameObject();
    } 

    public bool CheckIfFull()
    {
        int counter = 0;

        foreach (GameObject slot in quickSlotsList)
        {
            if(slot.transform.childCount > 1)
            {
                counter += 1;
            }
        }
        if(counter == 7)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
}
