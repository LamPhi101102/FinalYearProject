using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;
using UnityEngine.UI;
using TMPro;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem instance { get; set; }

    public GameObject InventoryScreen;
    public GameObject InventoryBag;
    public GameObject CraftingScreen;
    public GameObject CraftingCategoriesScreen;
    public List<GameObject> slotList = new List<GameObject>();
    public List<string> itemList = new List<string>();

    private GameObject itemToAdd;
    private GameObject whatSlotToEquip;

    public bool isOpen;


    //Pickup Alert
    public GameObject pickupAlert;
    public TextMeshProUGUI pickupName;
    public Image pickupImage;


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
            InventoryScreen.SetActive(true);
            CraftingScreen.SetActive(false);
            CraftingCategoriesScreen.SetActive(false);
            CraftingSystem.Instance.isOpen = false;
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

        TriggerPickupPopUp(ItemName, itemToAdd.GetComponent<Image>().sprite);
         

        ReCalculateList();
        CraftingSystem.Instance.RefreshNeededItems();
    }

    void TriggerPickupPopUp(string itemName, Sprite itemSprite)
    {
        pickupAlert.SetActive(true);
        pickupName.text = itemName + " x 1";
        pickupImage.sprite = itemSprite;
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

    public void RemoveItem(string nameToRemove, int amountToRemove)
    {
        int counter = amountToRemove;
        for (var i = slotList.Count - 1; i >= 0; i--)
        {
            if (slotList[i].transform.childCount > 1)
            {
                if (slotList[i].transform.GetChild(1).name == nameToRemove + "(Clone)" && counter != 0)
                {
                    Destroy(slotList[i].transform.GetChild(1).gameObject);
                    counter -= 1;
                }
            }
        }
        ReCalculateList();
        CraftingSystem.Instance.RefreshNeededItems();
    }

    public void ReCalculateList()
    {
        itemList.Clear();
        foreach (GameObject slot in slotList)
        {
            if (slot.transform.childCount > 1)
            {
                string name = slot.transform.GetChild(1).name; // Stone (Clone)
                string str2 = "(Clone)";

                string result = name.Replace(str2, "");
                itemList.Add(result);
            }
        }
    }
}
