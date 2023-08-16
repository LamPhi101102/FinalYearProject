using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

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
    Text AxeReq1, AxeReq2;

    public bool isOpen;

    // All Blueprint


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
        AxeReq1 = CraftingCategoriesScreen.transform.Find("Axe").transform.Find("Req1").GetComponent<Text>();
        AxeReq2 = CraftingCategoriesScreen.transform.Find("Axe").transform.Find("Req2").GetComponent<Text>();
        craftAxeButton = CraftingCategoriesScreen.transform.Find("Axe").transform.Find("Button").GetComponent<Button>();
        craftAxeButton.onClick.AddListener(delegate { CraftAnyItems(); });
    }

    void OpenToolsCategory()
    {
        CraftingScreen.SetActive(false);
        CraftingCategoriesScreen.SetActive(true);
    }

    void CraftAnyItems()
    {
        // add item into inventory


        // remove resouces from inventory
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && !isOpen)
        {
            CraftingScreen.SetActive(true);
            InventoryScreen.SetActive(false);
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
}
