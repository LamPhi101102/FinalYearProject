using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;
using TMPro;

public class InventoryItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public static InventoryItem Instance { get; set; }
    public bool isTrashable;

    private GameObject itemInfoUI;
    private TextMeshProUGUI itemInfoUI_itemName;
    private TextMeshProUGUI itemInfoUI_itemDescription;
    private TextMeshProUGUI itemInfoUI_itemFunctionality;

    public string thisName, thisDescription, thisFunctionality;

    private GameObject itemPendingConsumption;
    // Recovery Variable
    public bool isConsumable;
    public float healthEffect;
    public float staminaEffect;
    public float calorieseffect;
    // Increase Variable
    public float healthIncrease;
    public float staminaIncrease;

    public bool isEquippable;
    private GameObject itempendingEquipping;
    public bool isInsideQuickSlot;
    public bool wasInQuickSlot = false;

    public bool isSelected;

    // Start is called before the first frame update
    void Start()
    {
        itemInfoUI = InventorySystem.instance.itemInfoUi;
        itemInfoUI_itemName = itemInfoUI.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
        itemInfoUI_itemDescription = itemInfoUI.transform.Find("ItemDescription").GetComponent<TextMeshProUGUI>();
        itemInfoUI_itemFunctionality = itemInfoUI.transform.Find("ItemFunctionality").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (isSelected)
        {
            gameObject.GetComponent<DragDrop>().enabled = false;
        }
        else
        {
            gameObject.GetComponent<DragDrop>().enabled = true;
        }
    }


    // Event when we hover an item in inventory bag
    // it will open the itemInfoUI
    // Here it will get the text from itemInfoUI and then set the text follow the InventoryItem Script
    public void OnPointerEnter(PointerEventData eventData)
    {
        itemInfoUI.SetActive(true);
        itemInfoUI_itemName.text = thisName;
        itemInfoUI_itemDescription.text = thisDescription;
        itemInfoUI_itemFunctionality.text = thisFunctionality;
    }
    // Event when we exit mouse an item in inventory bag
    // It will close itemInfoUI
    public void OnPointerExit(PointerEventData eventData)
    {
        itemInfoUI.SetActive(false);
    }
    // Event when we right click on an item in inventory
    // it will recovery HP, Stamina and Calories
    public void OnPointerDown(PointerEventData eventData)
    {
       if(eventData.button == PointerEventData.InputButton.Right)
        {
            if (isConsumable)
            {
                itemPendingConsumption = gameObject;
                consumingFunction(healthEffect, staminaEffect, calorieseffect);
            }

            if (isEquippable == true && isInsideQuickSlot == false && EquipSystem.Instance.CheckIfFull() == false)
            {
                EquipSystem.Instance.AddToQuickSlots(gameObject);
                isInsideQuickSlot = true;
                consumingIncrease(healthIncrease, staminaIncrease);
            }
       }

    }
    // Event when we right click on an item in inventory
    // it will destroy this item and refresh inventory
    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (isConsumable && itemPendingConsumption == gameObject)
            {
                DestroyImmediate(gameObject);
                InventorySystem.instance.ReCalculateList();
                CraftingSystem.Instance.RefreshNeededItems();
            }
        }
    }

    private void consumingFunction(float healthEffect, float staminaEffect, float caloriesEffect)
    {
        itemInfoUI.SetActive(false);
        healhEffectCalculation(healthEffect);
        staminaEffectCalculation(staminaEffect);
        caloriesEffectCalculation(caloriesEffect);
    }
    // ==============================> Equip Armor Increase and Decrease <====================================
    public void consumingIncrease(float healthIncrease, float staminaIncrease)
    {
        itemInfoUI.SetActive(false);
        healhEffectIncrease(healthIncrease);
        StaminaEffectIncrease(staminaIncrease);
    }
    public void consumingDecrease(float healthIncrease, float staminaIncrease)
    {
        itemInfoUI.SetActive(false);
        HealthEffectDecrease(healthIncrease);
        StaminaEffectDecrease(staminaIncrease);
    }

    private static void healhEffectCalculation(float healthEffect)
    {
        // HP
        float healthBeforeConsumption = PlayerState.Instance.currentHP;
        float maxHealth = PlayerState.Instance.maxHP;

        if (healthEffect != 0)
        {
            if((healthBeforeConsumption + healthEffect) > maxHealth)
            {
                PlayerState.Instance.setHealth(maxHealth);
            }
            else
            {
                PlayerState.Instance.setHealth(healthBeforeConsumption + healthEffect);
            }
        }
    }
    // Set for Amor Category when equip gears
    private void healhEffectIncrease(float healthIncrease)
    {    
        float maxHealth = PlayerState.Instance.maxHP;
        PlayerState.Instance.setMaxHealth(maxHealth + healthIncrease);
    }
    // Set for Amor Category when unequip gears
    private void HealthEffectDecrease(float healthIncrease)
    {
        float maxHealth = PlayerState.Instance.maxHP;
        PlayerState.Instance.setMaxHealth(maxHealth - healthIncrease);
    }


    // Set for Amor Category when equip gears
    private void StaminaEffectIncrease(float staminaIncrease)
    {
        float maxStamina = PlayerState.Instance.maxStamina;
        PlayerState.Instance.setMaxStamina(maxStamina + staminaIncrease);
    }
    // Set for Amor Category when unequip gears
    private void StaminaEffectDecrease(float staminaIncrease)
    {
        float maxStamina = PlayerState.Instance.maxStamina;
        PlayerState.Instance.setMaxStamina(maxStamina - staminaIncrease);
    }
 

    private static void staminaEffectCalculation(float staminaEffect)
    {
        float staminaBeforeConsumption = PlayerState.Instance.currentStamina;
        float maxStamina = PlayerState.Instance.maxStamina;

        if(staminaEffect != 0)
        {
            if ((staminaBeforeConsumption + staminaEffect) > maxStamina)
            {
                PlayerState.Instance.setStamina(maxStamina);
            }
            else
            {
                PlayerState.Instance.setStamina(staminaBeforeConsumption + staminaEffect);
            }
        }
    }

    private static void caloriesEffectCalculation(float caloriesEffect)
    {
        float caloriesBeforeConsumption = PlayerState.Instance.currentCaloriesPercent;
        float maxCalories = PlayerState.Instance.maxCaloriesPercent;

        if(caloriesEffect != 0)
        {
            if ((caloriesBeforeConsumption + caloriesEffect) > maxCalories)
            {
                PlayerState.Instance.setCalories(maxCalories);
            }
            else
            {
                PlayerState.Instance.setCalories(caloriesBeforeConsumption + caloriesEffect);
            }
        }
    }
}
