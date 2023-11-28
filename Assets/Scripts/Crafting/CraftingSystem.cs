using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftingSystem : MonoBehaviour
{

    public GameObject CraftingScreen;
    public GameObject ShopScreen;
    public GameObject MenuUI;
    public GameObject MenuQuest;
    public GameObject InventoryScreen;
    public GameObject CraftingCategoriesScreen;
    public GameObject CraftingHelmetScreen;
    public GameObject CraftingArmorScreen;
    public GameObject CraftingGloveScreen;
    public GameObject CraftingBootsScreen;
    public GameObject CraftingRingScreen;


    public List<string> inventoryItemList = new List<string>();

    //  Category Button
    Button toolsBTN, helmetBTN, armorBTN, gloveBTN, bootsBTN, ringBTN, 
        arrowBackWeapon, arrowBackHelmet, arrowBackArmor, 
        arrowBackGlove, arrowBackBoots, arrowBackRing;

    // Craft Button
    Button craftAxeButton, craftStoneHelmetButton, craftStoneArmorButton,
        craftWoodenGlovesButton, craftBootsButton, craftGoldRingButton;

    //Requirement Text
    TextMeshProUGUI AxeReq1, AxeReq2, 
        StoneHelmetReq1, StoneHelmetReq2,
        StoneArmorReq1, StoneArmorReq2,
        WoodenGlovesReq1, WoodenGlovesReq2,
        BootsReq1, BootsReq2,
        GoldRingReq1, GoldRingReq2;

    public bool isOpen;

    // All Blueprint
    // Blueprint for Weapon
    public BluePrint AxeBLP = new BluePrint("Axe", 2, "Stone", 3, "Stick", 3);
    // Blueprint for Helmet
    public BluePrint StoneHelmet = new BluePrint("Stone Helmet", 2, "Stone", 5, "Stick", 3);
    // Blueprint for Armor
    public BluePrint StoneArmor = new BluePrint("Stone Armor", 2, "Stone", 10, "Stick", 5);
    // Blueprint for Glove
    public BluePrint WoodenGloves = new BluePrint("Wooden Gloves", 2, "Stone", 4, "Stick", 2);
    // Blueprint for Boots
    public BluePrint Boots = new BluePrint("Boots", 2, "Stone", 6, "Stick", 4);
    // Blueprint for Ring
    public BluePrint GoldRing = new BluePrint("Gold Ring", 2, "Stone", 1, "Stick", 1);
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
        // ================================= 6 Screen for Crafting in Crafting MainMenu ====================================
        // Weapon Button
        toolsBTN = CraftingScreen.transform.Find("WeaponButton").GetComponent<Button>();
        toolsBTN.onClick.AddListener(delegate { OpenToolsCategory(); });
        // Helmet Button
        helmetBTN = CraftingScreen.transform.Find("HelmetButton").GetComponent<Button>();
        helmetBTN.onClick.AddListener(delegate { OpenHelmetCategory(); });
        // Armor Button
        armorBTN = CraftingScreen.transform.Find("ArmorButton").GetComponent<Button>();
        armorBTN.onClick.AddListener(delegate { OpenArmorCategory(); });
        // Glove Button
        gloveBTN = CraftingScreen.transform.Find("GloveButton").GetComponent<Button>();
        gloveBTN.onClick.AddListener(delegate { OpenGloveCategory(); });
        // Boots Button
        bootsBTN = CraftingScreen.transform.Find("BootsButton").GetComponent<Button>();
        bootsBTN.onClick.AddListener(delegate { OpenBootsCategory(); });
        // Ring Button
        ringBTN = CraftingScreen.transform.Find("RingButton").GetComponent<Button>();
        ringBTN.onClick.AddListener(delegate { OpenRingCategory(); });

        // ================================ Arrow Back in Weapon Menu ====================================================
        arrowBackWeapon = CraftingCategoriesScreen.transform.Find("ArrowBackWeapon").GetComponent<Button>();
        arrowBackWeapon.onClick.AddListener(delegate { CloseToolsCategory(); });

        // ================================ Arrow Back in Helmet Menu ====================================================
        arrowBackHelmet = CraftingHelmetScreen.transform.Find("ArrowBackHelmet").GetComponent<Button>();
        arrowBackHelmet.onClick.AddListener(delegate { CloseHelmetCategory(); });

        // ================================ Arrow Back in Armor Menu ====================================================
        arrowBackArmor = CraftingArmorScreen.transform.Find("ArrowBackArmor").GetComponent<Button>();
        arrowBackArmor.onClick.AddListener(delegate { CloseArmorCategory(); });

        // ================================ Arrow Back in Glove Menu ====================================================
        arrowBackGlove = CraftingGloveScreen.transform.Find("ArrowBackGlove").GetComponent<Button>();
        arrowBackGlove.onClick.AddListener(delegate { CloseGloveCategory(); });

        // ================================ Arrow Back in Boots Menu ====================================================
        arrowBackBoots = CraftingBootsScreen.transform.Find("ArrowBackBoots").GetComponent<Button>();
        arrowBackBoots.onClick.AddListener(delegate { CloseBootsCategory(); });
        // ================================ Arrow Back in Ring Menu ====================================================
        arrowBackRing = CraftingRingScreen.transform.Find("ArrowBackRing").GetComponent<Button>();
        arrowBackRing.onClick.AddListener(delegate { CloseRingCategory(); });



        // Weapon =====> Axe
        AxeReq1 = CraftingCategoriesScreen.transform.Find("Axe").transform.Find("AxeReq1").GetComponent<TextMeshProUGUI>();
        AxeReq2 = CraftingCategoriesScreen.transform.Find("Axe").transform.Find("AxeReq2").GetComponent<TextMeshProUGUI>();
        craftAxeButton = CraftingCategoriesScreen.transform.Find("Axe").transform.Find("craftAxeButton").GetComponent<Button>();
        craftAxeButton.onClick.AddListener(delegate { CraftAnyItems(AxeBLP); });
        // Helmet =====> Stone Helmet
        StoneHelmetReq1 = CraftingHelmetScreen.transform.Find("StoneHelmet").transform.Find("StoneHelmetReq1").GetComponent<TextMeshProUGUI>();
        StoneHelmetReq2 = CraftingHelmetScreen.transform.Find("StoneHelmet").transform.Find("StoneHelmetReq2").GetComponent<TextMeshProUGUI>();
        craftStoneHelmetButton = CraftingHelmetScreen.transform.Find("StoneHelmet").transform.Find("craftStoneHelmetButton").GetComponent<Button>();
        craftStoneHelmetButton.onClick.AddListener(delegate { CraftAnyItems(StoneHelmet); });
        // Armor ======> Stone Armor
        StoneArmorReq1 = CraftingArmorScreen.transform.Find("StoneArmor").transform.Find("StoneArmorReq1").GetComponent<TextMeshProUGUI>();
        StoneArmorReq2 = CraftingArmorScreen.transform.Find("StoneArmor").transform.Find("StoneArmorReq2").GetComponent<TextMeshProUGUI>();
        craftStoneArmorButton = CraftingArmorScreen.transform.Find("StoneArmor").transform.Find("craftStoneArmorButton").GetComponent<Button>();
        craftStoneArmorButton.onClick.AddListener(delegate { CraftAnyItems(StoneArmor); });
        // Gloves =====> Wooden Glove
        WoodenGlovesReq1 = CraftingGloveScreen.transform.Find("WoodenGloves").transform.Find("WoodenGlovesReq1").GetComponent<TextMeshProUGUI>();
        WoodenGlovesReq2 = CraftingGloveScreen.transform.Find("WoodenGloves").transform.Find("WoodenGlovesReq2").GetComponent<TextMeshProUGUI>();
        craftWoodenGlovesButton = CraftingGloveScreen.transform.Find("WoodenGloves").transform.Find("craftWoodenGlovesButton").GetComponent<Button>();
        craftWoodenGlovesButton.onClick.AddListener(delegate { CraftAnyItems(WoodenGloves); });
        // Boots =====> Boots
        BootsReq1 = CraftingBootsScreen.transform.Find("Boots").transform.Find("BootsReq1").GetComponent<TextMeshProUGUI>();
        BootsReq2 = CraftingBootsScreen.transform.Find("Boots").transform.Find("BootsReq2").GetComponent<TextMeshProUGUI>();
        craftBootsButton = CraftingBootsScreen.transform.Find("Boots").transform.Find("craftBootsButton").GetComponent<Button>();
        craftBootsButton.onClick.AddListener(delegate { CraftAnyItems(Boots); });
        // Ring =====> Gold Ring
        GoldRingReq1 = CraftingRingScreen.transform.Find("GoldRing").transform.Find("GoldRingReq1").GetComponent<TextMeshProUGUI>();
        GoldRingReq2 = CraftingRingScreen.transform.Find("GoldRing").transform.Find("GoldRingReq2").GetComponent<TextMeshProUGUI>();
        craftGoldRingButton = CraftingRingScreen.transform.Find("GoldRing").transform.Find("craftGoldRingButton").GetComponent<Button>();
        craftGoldRingButton.onClick.AddListener(delegate { CraftAnyItems(GoldRing); });
    }

    // Crafting Screen For Weapon
    void OpenToolsCategory()
    {
        CraftingScreen.SetActive(false);
        CraftingCategoriesScreen.SetActive(true);
    }
    void CloseToolsCategory()
    {
        CraftingScreen.SetActive(true);
        CraftingCategoriesScreen.SetActive(false);
    }
    // Crafting Screen For Helmet
    void OpenHelmetCategory()
    {
        CraftingScreen.SetActive(false);
        CraftingHelmetScreen.SetActive(true);
    }
    void CloseHelmetCategory()
    {
        CraftingScreen.SetActive(true);
        CraftingHelmetScreen.SetActive(false);
    }
    // Crafting Screen For Armor
    void OpenArmorCategory()
    {
        CraftingScreen.SetActive(false);
        CraftingArmorScreen.SetActive(true);
    }
    void CloseArmorCategory()
    {
        CraftingScreen.SetActive(true);
        CraftingArmorScreen.SetActive(false);
    }
    // Crafting Screen For Glove
    void OpenGloveCategory()
    {
        CraftingScreen.SetActive(false);
        CraftingGloveScreen.SetActive(true);
    }
    void CloseGloveCategory()
    {
        CraftingScreen.SetActive(true);
        CraftingGloveScreen.SetActive(false);
    }
    // Crafting Screen For Boots
    void OpenBootsCategory()
    {
        CraftingScreen.SetActive(false);
        CraftingBootsScreen.SetActive(true);
    }
    void CloseBootsCategory()
    {
        CraftingScreen.SetActive(true);
        CraftingBootsScreen.SetActive(false);
    }
    // Crafting Screen For Boots
    void OpenRingCategory()
    {
        CraftingScreen.SetActive(false);
        CraftingRingScreen.SetActive(true);
    }
    void CloseRingCategory()
    {
        CraftingScreen.SetActive(true);
        CraftingRingScreen.SetActive(false);
    }



    void CraftAnyItems(BluePrint blueprintToCraft)
    {
        SoundManager.instance.PlayDropSound(SoundManager.instance.craftingItemSound);
        StartCoroutine(craftedDelayforSound());
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
    IEnumerator craftedDelayforSound()
    {
        yield return new WaitForSeconds(1f);
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
        //RefreshNeededItems();

        if (Input.GetKeyDown(KeyCode.I) && !isOpen)
        {
            CraftingScreen.SetActive(true);
            InventoryScreen.SetActive(false);
            ShopScreen.SetActive(false);
            MenuUI.SetActive(false);
            MenuQuest.SetActive(false);
            InventorySystem.instance.isOpen = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            InventorySystem.instance.isQuestMenuOpen = false;
            InventorySystem.instance.isOpen = false;
            InventorySystem.instance.isShopOpen = false;
            isOpen = true;
            SelectionManager.Instance.DisableSelection();
            SelectionManager.Instance.GetComponent<SelectionManager>().enabled = false;
            
        }
        else if (Input.GetKeyDown(KeyCode.I) && isOpen)
        {
            CraftingScreen.SetActive(false);
            CraftingCategoriesScreen.SetActive(false);
            CraftingHelmetScreen.SetActive(false);
            CraftingArmorScreen.SetActive(false);
            CraftingGloveScreen.SetActive(false);
            CraftingBootsScreen.SetActive(false);
            CraftingRingScreen.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            isOpen = false;
            Cursor.visible = false;
            SelectionManager.Instance.EnableSelection();
            SelectionManager.Instance.GetComponent<SelectionManager>().enabled = true;
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
        // =================================================== Weapon ======================================================
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
        // =================================================== Helmet ======================================================
        // =========== Stone Helmet ===========
        StoneHelmetReq1.text = "5 Stone [" + stoneCount + "]";
        StoneHelmetReq2.text = "3 Stick [" + stickCount + "]";

        if (stoneCount >= 5 && stickCount >= 3)
        {
            craftStoneHelmetButton.gameObject.SetActive(true);
        }
        else
        {
            craftStoneHelmetButton.gameObject.SetActive(false);
        }
        // =================================================== Armor ======================================================
        // =========== Stone Armor ===========
        StoneArmorReq1.text = "10 Stone [" + stoneCount + "]";
        StoneArmorReq2.text = "5 Stick [" + stickCount + "]";

        if (stoneCount >= 10 && stickCount >= 5)
        {
            craftStoneArmorButton.gameObject.SetActive(true);
        }
        else
        {
            craftStoneArmorButton.gameObject.SetActive(false);
        }
        // =================================================== Glove ======================================================
        // =========== Wooden Gloves ===========
        WoodenGlovesReq1.text = "4 Stone [" + stoneCount + "]";
        WoodenGlovesReq2.text = "2 Stick [" + stickCount + "]";

        if (stoneCount >= 4 && stickCount >= 2)
        {
            craftWoodenGlovesButton.gameObject.SetActive(true);
        }
        else
        {
            craftWoodenGlovesButton.gameObject.SetActive(false);
        }
        // =================================================== Boots ======================================================
        // =========== Wooden Gloves ===========
        BootsReq1.text = "6 Stone [" + stoneCount + "]";
        BootsReq2.text = "4 Stick [" + stickCount + "]";

        if (stoneCount >= 6 && stickCount >= 4)
        {
            craftBootsButton.gameObject.SetActive(true);
        }
        else
        {
            craftBootsButton.gameObject.SetActive(false);
        }
        // =================================================== Ring ======================================================
        // =========== Gold Ring ===========
        GoldRingReq1.text = "1 Stone [" + stoneCount + "]";
        GoldRingReq2.text = "1 Stick [" + stickCount + "]";

        if (stoneCount >= 1 && stickCount >= 1)
        {
            craftGoldRingButton.gameObject.SetActive(true);
        }
        else
        {
            craftGoldRingButton.gameObject.SetActive(false);
        }
    }
}
