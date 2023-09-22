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
        Debug.Log("OnDrop");

        if (!Item)
        {         
            // Check if the item being dragged is equippable
            if (DragDrop.itemBeingDragged.GetComponent<InventoryItem>().isEquippable == true)
            {
                DragDrop.itemBeingDragged.transform.SetParent(transform);
                DragDrop.itemBeingDragged.transform.localPosition = new Vector2(0, 0);

                if (transform.CompareTag("QuickSlot") == false)
                {
                    DragDrop.itemBeingDragged.GetComponent<InventoryItem>().isInsideQuickSlot = false;
                    InventorySystem.instance.ReCalculateList();
                }
                if (transform.CompareTag("QuickSlot"))
                {
                    DragDrop.itemBeingDragged.GetComponent<InventoryItem>().isInsideQuickSlot = true;
                    InventorySystem.instance.ReCalculateList();
                }
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
