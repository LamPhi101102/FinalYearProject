using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class InventoryItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public bool isTrashable;

    private GameObject itemInfoUI;
    private TextMeshProUGUI itemInfoUI_itemName;
    private TextMeshProUGUI itemInfoUI_itemDescription;
    private TextMeshProUGUI itemInfoUI_itemFunctionality;

    public string thisName, thisDescription, thisFunctionality;

    private GameObject itemPendingConsumption;

    public bool isConsumable;
    public float healthEffect;
    public float staminaEffect;
    public float calorieseffect;


    // Start is called before the first frame update
    void Start()
    {
        itemInfoUI = InventorySystem.instance.itemInfoUi;
        itemInfoUI_itemName = itemInfoUI.transform.Find("ItemName").GetComponent<TextMeshProUGUI>();
        itemInfoUI_itemDescription = itemInfoUI.transform.Find("ItemDescription").GetComponent<TextMeshProUGUI>();
        itemInfoUI_itemFunctionality = itemInfoUI.transform.Find("ItemFunctionality").GetComponent<TextMeshProUGUI>();
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

    // Update is called once per frame
    void Update()
    {
       
    }
}
