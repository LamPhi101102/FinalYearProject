using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem instance { get; set; }

    public GameObject InventoryScreen;
    public GameObject InventoryBag;
    public List<GameObject> slotList = new List<GameObject>();
    public List<string> itemList = new List<string>();

    private GameObject itemToAdd;
    private GameObject whatSlotToEquip;

    public bool isOpen;

    //public bool isFull;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else{
            instance = this;
        }
    }
    void Start()
    {
        isOpen = false;
        PopulateSlotList();
    }

    private void PopulateSlotList()
    {
        foreach (Transform child in InventoryBag.transform)
        {
            if (child.CompareTag("Slot"))
            {
                slotList.Add(child.gameObject);
            }
        }
    }



    void Update()
    {
        if(Input.GetKeyDown(KeyCode.B) && !isOpen)
        {
            Debug.Log("B is pressed");
            InventoryScreen.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            isOpen = true;
        }else if(Input.GetKeyDown(KeyCode.B) &&  isOpen)
        {
            InventoryScreen.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            isOpen = false;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isOpen)
        {
            InventoryScreen.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            isOpen = false;
        }
    }



    public void AddToInventory(string ItemName)
    {
        whatSlotToEquip = FindNextEmptySlot();
        itemToAdd = Instantiate(Resources.Load<GameObject>(ItemName), whatSlotToEquip.transform.position, whatSlotToEquip.transform.rotation);
        itemToAdd.transform.SetParent(whatSlotToEquip.transform);
        itemList.Add(ItemName);
    }

    private GameObject FindNextEmptySlot()
    {
        foreach (GameObject slot in slotList)
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

        foreach (GameObject slot in slotList)
        {
            if (slot.transform.childCount > 1)
            {
                counter += 1;
            }      
        }

        if (counter == 40)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
