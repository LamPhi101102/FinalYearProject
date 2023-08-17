using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftingSystem : MonoBehaviour
{
    public GameObject CraftingScreen;
    public GameObject InventoryScreen;
    public GameObject CraftingCategoriesScreen;

    public List<string> inventoryItemList = new List<string>();

    //  Category Button
    Button toolsBTN;

    // Craft Button
    Button craftAxeButton;

    //Requirement Text
    TextMeshProUGUI AxeReq1, AxeReq2;

    public bool isOpen;

    // All Blueprint
    
    public BluePrint AxeBLP = new BluePrint("Axe", 2, "Stone", 3, "Stick", 3);

    public static CraftingSystem Instance { get; set; }



    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        isOpen = false;

        toolsBTN = CraftingScreen.transform.Find("WeaponButton").GetComponent<Button>();
        toolsBTN.onClick.AddListener(delegate { OpenToolsCategory(); });

        // Axe
        AxeReq1 = CraftingCategoriesScreen.transform.Find("Axe").transform.Find("AxeReq1").GetComponent<TextMeshProUGUI>();
        AxeReq2 = CraftingCategoriesScreen.transform.Find("Axe").transform.Find("AxeReq2").GetComponent<TextMeshProUGUI>();
        craftAxeButton = CraftingCategoriesScreen.transform.Find("Axe").transform.Find("craftAxeButton").GetComponent<Button>();
        craftAxeButton.onClick.AddListener(delegate { CraftAnyItems(AxeBLP); });
    }

    void OpenToolsCategory()
    {
        CraftingScreen.SetActive(false);
        CraftingCategoriesScreen.SetActive(true);
    }

    void CraftAnyItems(BluePrint blueprintToCraft)
    {
        // add item into inventory
        InventorySystem.instance.AddToInventory(blueprintToCraft.itemName);

        // remove resouces from inventory
        if (blueprintToCraft.numOfRequirements == 1) {
            
            InventorySystem.instance.RemoveItem(blueprintToCraft.Req1, blueprintToCraft.Req1amount);
        }else if(blueprintToCraft.numOfRequirements == 2)
        {
            InventorySystem.instance.RemoveItem(blueprintToCraft.Req1, blueprintToCraft.Req1amount);
            InventorySystem.instance.RemoveItem(blueprintToCraft.Req2, blueprintToCraft.Req2amount);
        }
        //refrsh list
        StartCoroutine(calculate());
    }

    public IEnumerator calculate()
    {
        yield return 0;
        InventorySystem.instance.ReCalculateList();
        RefreshNeededItems();
    }

    // Update is called once per frame
    void Update()
    {
        RefreshNeededItems();

        if (Input.GetKeyDown(KeyCode.I) && !isOpen)
        {
            CraftingScreen.SetActive(true);
            InventoryScreen.SetActive(false);
            InventorySystem.instance.isOpen = false;
            Cursor.lockState = CursorLockMode.None;
            isOpen = true;
        }
        else if (Input.GetKeyDown(KeyCode.I) && isOpen)
        {
            CraftingScreen.SetActive(false);
            CraftingCategoriesScreen.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            isOpen = false;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isOpen)
        {
            CraftingScreen.SetActive(false);
            CraftingCategoriesScreen.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            isOpen = false;
        }
    }

    public void RefreshNeededItems()
    {
        int stoneCount = 0;
        int stickCount = 0;

        inventoryItemList = InventorySystem.instance.itemList;
        foreach (string itemName in inventoryItemList)
        {
            switch (itemName)
            {
                case "Stone":
                    stoneCount += 1;
                    break;
                case "Stick":
                    stickCount += 1;
                    break;
            }
        }
        // -------- Axe ----------
        AxeReq1.text = "3 Stone [" + stoneCount + "]";
        AxeReq2.text = "3 Stick [" + stickCount + "]";

        if (stoneCount >= 3 && stickCount >= 3)
        {
            craftAxeButton.gameObject.SetActive(true);
        }
        else
        {
            craftAxeButton.gameObject.SetActive(false);
        }
    }
}
