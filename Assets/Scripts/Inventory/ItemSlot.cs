using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Debug = UnityEngine.Debug;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    public GameObject Item
    {
        get
        {
            // to check if there is a item in this slots it cannot place
            if(transform.childCount > 1)
            {
                return transform.GetChild(0).gameObject;
            }
            return null;
        }
    }
    public void OnDrop(PointerEventData eventData)
    {

        if (!Item)
        {
            InventoryItem draggedItem = DragDrop.itemBeingDragged.GetComponent<InventoryItem>();

            // Check if the item being dragged is equippable
            if (draggedItem.isEquippable)
            {
                DragDrop.itemBeingDragged.transform.SetParent(transform);
                DragDrop.itemBeingDragged.transform.localPosition = Vector2.zero;

                if (transform.CompareTag("QuickSlot"))
                {
                    if (!draggedItem.isInsideQuickSlot)
                    {
                        draggedItem.isInsideQuickSlot = true;
                        draggedItem.consumingIncrease(draggedItem.healthIncrease, draggedItem.staminaIncrease);
                    }
                }
                else
                {
                    if (draggedItem.isInsideQuickSlot)
                    {
                        draggedItem.isInsideQuickSlot = false;
                        draggedItem.consumingDecrease(draggedItem.healthIncrease, draggedItem.staminaIncrease);
                    }
                }

                InventorySystem.instance.ReCalculateList();
            }
            else if(transform.CompareTag("Slot"))
            {
                DragDrop.itemBeingDragged.transform.SetParent(transform);
                DragDrop.itemBeingDragged.transform.localPosition = new Vector2(0, 0);
                Debug.Log("Cannot drop non-equippable item into this slot.");
            }
        }
    }
}
