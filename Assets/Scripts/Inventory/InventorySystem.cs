using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using StarterAssets;

public class InventorySystem : MonoBehaviour
{
    public GameObject itemInfoUi;
    public static InventorySystem instance { get; set; }


    public GameObject InventoryScreen;
    public GameObject InventoryBag;
    public GameObject CraftingScreen;
    public GameObject ShopScreen;
    public GameObject MenuUI;
    public GameObject questMenu;
    public bool isQuestMenuOpen;
    public GameObject CraftingCategoriesScreen;
    public List<GameObject> slotList = new List<GameObject>();
    public List<string> itemList = new List<string>();

    private GameObject itemToAdd;
    private GameObject whatSlotToEquip;

    public bool isOpen;
    public bool isShopOpen;


    //Pickup Alert
    public GameObject pickupAlert;
    public TextMeshProUGUI pickupName;
    public Image pickupImage;
    public TextMeshProUGUI pickupCountText;


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
        isShopOpen = false;
        PopulateSlotList();

        Cursor.visible = false;
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
            ShopScreen.SetActive(false);
            questMenu.SetActive(false);
            MenuUI.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;  
            isOpen = true;
            isQuestMenuOpen = false;
            CraftingSystem.Instance.isOpen = false;
            isShopOpen = false;
            SelectionManager.Instance.DisableSelection();
            SelectionManager.Instance.GetComponent<SelectionManager>().enabled = false;
        }
        else if(Input.GetKeyDown(KeyCode.B) &&  isOpen)
        {
            InventoryScreen.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;       
            isOpen = false;
            Cursor.visible = false;

            SelectionManager.Instance.EnableSelection();
            SelectionManager.Instance.GetComponent<SelectionManager>().enabled = true;
        }
        

        if (Input.GetKeyDown(KeyCode.H) && !isShopOpen)
        {
            ShopScreen.SetActive(true);
            InventoryScreen.SetActive(false);
            CraftingScreen.SetActive(false);
            questMenu.SetActive(false);
            CraftingCategoriesScreen.SetActive(false);
            CraftingSystem.Instance.isOpen = false;
            MenuUI.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            isShopOpen = true;
            isOpen = false;
            isQuestMenuOpen = false;
            CraftingSystem.Instance.isOpen = false;
            SelectionManager.Instance.DisableSelection();
            SelectionManager.Instance.GetComponent<SelectionManager>().enabled = false;
        }
        else if (Input.GetKeyDown(KeyCode.H) && isShopOpen)
        {
            ShopScreen.SetActive(false);
            InventoryScreen.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;

            isShopOpen = false;

            SelectionManager.Instance.EnableSelection();
            SelectionManager.Instance.GetComponent<SelectionManager>().enabled = true;
        }
        
        if (Input.GetKeyDown(KeyCode.Q) && !isQuestMenuOpen)
        {
            questMenu.SetActive(true);
            InventoryScreen.SetActive(false);
            CraftingScreen.SetActive(false);
            CraftingCategoriesScreen.SetActive(false);
            CraftingSystem.Instance.isOpen = false;
            ShopScreen.SetActive(false);
            MenuUI.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            isOpen = false;
            CraftingSystem.Instance.isOpen = false;
            isShopOpen = false;
            isQuestMenuOpen = true;
            SelectionManager.Instance.DisableSelection();
            SelectionManager.Instance.GetComponent<SelectionManager>().enabled = false;
        }
        else if (Input.GetKeyDown(KeyCode.Q) && isQuestMenuOpen)
        {
            questMenu.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            isQuestMenuOpen = false;

            SelectionManager.Instance.EnableSelection();
            SelectionManager.Instance.GetComponent<SelectionManager>().enabled = true;
        }
        
    }



    public void AddToInventory(string ItemName)
    {
        if (SaveManager.instance.isLoading == false)
        {
            SoundManager.instance.PlayDropSound(SoundManager.instance.pickUpItemSound);
        }
               
        whatSlotToEquip = FindNextEmptySlot();
        itemToAdd = Instantiate(Resources.Load<GameObject>(ItemName), whatSlotToEquip.transform.position, whatSlotToEquip.transform.rotation);
        itemToAdd.transform.SetParent(whatSlotToEquip.transform);
        itemList.Add(ItemName);

        TriggerPickupPopUp(ItemName, itemToAdd.GetComponent<Image>().sprite);

        StartCoroutine(ClosePickupAlertAfterDelay());

        ReCalculateList();
        CraftingSystem.Instance.RefreshNeededItems();

        QuestManager.instance.RefreshTrackerList();

    }

    IEnumerator ClosePickupAlertAfterDelay()
    {
        yield return new WaitForSeconds(1f); // Wait for 3 seconds
        pickupAlert.SetActive(false); // Deactivate the pickupAlert
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

        if (counter == 39)
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
                    DestroyImmediate(slotList[i].transform.GetChild(1).gameObject);
                    counter -= 1;
                }
            }
        }
        ReCalculateList();
        CraftingSystem.Instance.RefreshNeededItems();

        QuestManager.instance.RefreshTrackerList();
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

    public int CheckItemAmount(string name)
    {
        int itemCounter = 0;
        foreach(string item in itemList)
        {
            if(item == name)
            {
                itemCounter++;
            }
        }
        return itemCounter;
    }
}
