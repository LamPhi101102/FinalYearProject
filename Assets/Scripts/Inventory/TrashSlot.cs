using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TrashSlot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject trashalertUI;

    private TextMeshProUGUI textToModify;

    public Sprite trash_closed;
    public Sprite trash_opened;

    private Image imageComponent;

    Button YesBTN, NoBTN;

    GameObject draggedItem
    {
        get
        {
            return DragDrop.itemBeingDragged;
        }
    }

    GameObject itemToBeDeleted;


    public string itemName
    {
        get
        {
            string name = itemToBeDeleted.name;
            string toRemove = "(Clone)";
            string result = name.Replace(toRemove, "");
            return result;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Get Text and Button in TrashAlertUI and then carryout the function inside
        imageComponent = transform.Find("BackGround").GetComponent<Image>();

        textToModify = trashalertUI.transform.Find("DeletionAlertText").GetComponent<TextMeshProUGUI>();

        YesBTN = trashalertUI.transform.Find("Yes").GetComponent<Button>();
        YesBTN.onClick.AddListener(delegate { DeleteItem(); });

        NoBTN = trashalertUI.transform.Find("No").GetComponent<Button>();
        NoBTN.onClick.AddListener(delegate { CancelDeletion(); });
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (draggedItem.GetComponent<InventoryItem>().isTrashable == true)
        {
            // assign item that we drag to a variable that will  perform delete function
            itemToBeDeleted = draggedItem.gameObject;
            StartCoroutine(notifyBeforeDeletion());
        }
    }

    IEnumerator notifyBeforeDeletion()
    {
        trashalertUI.SetActive(true);
        // get and set text to notify
        textToModify.text = "Throw away this " + itemName + " right?";
        // wait for (time) seconds
        yield return new WaitForSeconds(1f);
    }

    private void CancelDeletion()
    {
        imageComponent.sprite = trash_closed;
        trashalertUI.SetActive(false);
    }

    private void DeleteItem()
    {
        // Delete item
        imageComponent.sprite = trash_closed;
        DestroyImmediate(itemToBeDeleted.gameObject);
        // Refresh in inventory
        InventorySystem.instance.ReCalculateList();
        CraftingSystem.Instance.RefreshNeededItems();
        // close trashalertUI
        trashalertUI.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // use this event to change icon when we drag an item into trash
        if (draggedItem != null && draggedItem.GetComponent<InventoryItem>().isTrashable == true)
        {
            imageComponent.sprite = trash_opened;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // use this event return the default trash because there is no an item that is dragged into trash
        if (draggedItem != null && draggedItem.GetComponent<InventoryItem>().isTrashable == true)
        {
            imageComponent.sprite = trash_closed;
        }
    }
}
