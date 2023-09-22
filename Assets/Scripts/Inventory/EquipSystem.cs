using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class EquipSystem : MonoBehaviour
{
    public static EquipSystem Instance { get; set; }

    public GameObject quickSlotsPanel;
    public List<GameObject> quickSlotsList = new List<GameObject>();


    public GameObject numberHolder;

    // it is not select because it is not in range of quickslot
    public int selectedNumber = -1;
    public GameObject selectedItem;


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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectQuickSlot(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectQuickSlot(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectQuickSlot(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SelectQuickSlot(4);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SelectQuickSlot(5);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            SelectQuickSlot(6);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            SelectQuickSlot(7);
        }
    }

    void SelectQuickSlot(int number)
    {
        if (checkIfSlotisFull(number) == true)
        {
            if (selectedNumber != number)
            {
                selectedNumber = number;
                // Unselected previously selected item
                if (selectedItem != null)
                {
                    selectedItem.gameObject.GetComponent<InventoryItem>().isSelected = false;
                }
                selectedItem = getSelectedItem(number);
                selectedItem.GetComponent<InventoryItem>().isSelected = true;

                // Changing the color
                foreach (Transform child in numberHolder.transform)
                {
                    child.transform.Find("Number").GetComponent<TextMeshProUGUI>().color = Color.white;
                }

                TextMeshProUGUI toBeChanged = numberHolder.transform.Find("QuickSlotNumberFrame"+number).transform.Find("Number").GetComponent<TextMeshProUGUI>();
                toBeChanged.color = Color.yellow;
            }
            // we are trying to select the same slot
            else
            {
                // nothing selected
                selectedNumber = -1;
                // Unselected previously selected item
                if (selectedItem != null)
                {
                    selectedItem.gameObject.GetComponent<InventoryItem>().isSelected = false;
                    selectedItem = null;
                }
                // Changing the color
                foreach (Transform child in numberHolder.transform)
                {
                    child.transform.Find("Number").GetComponent<TextMeshProUGUI>().color = Color.white;
                }

            }
        }    
    }

    GameObject getSelectedItem(int slotNumber)
    {
        return quickSlotsList[slotNumber - 1].transform.GetChild(1).gameObject;
    }


    bool checkIfSlotisFull(int slotNumber)
    {
        if (quickSlotsList[slotNumber - 1].transform.childCount > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
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
