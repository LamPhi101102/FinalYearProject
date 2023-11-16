using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.Diagnostics;

public class EquipSystem : MonoBehaviour
{
    public static EquipSystem Instance { get; set; }

    public GameObject quickSlotsPanel;
    public List<GameObject> quickSlotsList = new List<GameObject>();

    // Bow Weapon
    public GameObject bow;
    public GameObject containBow;
    // Sword Weapon
    public GameObject sword;

    public GameObject numberHolder;

    // it is not select because it is not in range of quickslot
    public int selectedNumber = -1;
    public GameObject selectedItem;
    public bool isBowEquip = false;
    public bool isSwordEquip = false;
    public bool isAxeEquip = false;


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
                if(selectedItem != null)
                {
                    selectedItem.GetComponent<InventoryItem>().isSelected = true;
                    // Changing the color
                    foreach (Transform child in numberHolder.transform)
                    {
                        child.transform.Find("Number").GetComponent<TextMeshProUGUI>().color = Color.white;
                    }

                    TextMeshProUGUI toBeChanged = numberHolder.transform.Find("QuickSlotNumberFrame" + number).transform.Find("Number").GetComponent<TextMeshProUGUI>();

                    if (selectedItem.CompareTag("BowWeapon"))
                    {
                        bow.SetActive(true);
                        containBow.SetActive(true);
                        isBowEquip = true;
                    }
                    else
                    {
                        isBowEquip = false;
                    }
                    if (selectedItem.CompareTag("Sword"))
                    {
                        sword.SetActive(true);
                        isSwordEquip = true;
                    }
                    else
                    {
                        isSwordEquip = false;
                    }
                    toBeChanged.color = Color.yellow;
                }            
   
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
                bow.SetActive(false);
                containBow.SetActive(false);
                isSwordEquip = false;

                isBowEquip = false;
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
        if (slotNumber > 0 && slotNumber <= quickSlotsList.Count)
        {
            Transform slotTransform = quickSlotsList[slotNumber - 1].transform;
            if (slotTransform.childCount > 1)
            {
                return slotTransform.GetChild(1).gameObject;
            }
            else
            {
                return null; // Return null or handle the absence of an item in the slot
            }
        }
        else
        {
            return null; // Return null or handle the out-of-range slot number
        }
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
