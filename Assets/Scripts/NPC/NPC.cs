using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Debug = UnityEngine.Debug;

public class NPC : MonoBehaviour
{
    public bool playerInRange;

    public bool isTalkingWithPlayer;

    public string ItemName;

    public string GetItemName()
    {
        return ItemName;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    public void StartConservation()
    {
        isTalkingWithPlayer = true;
        Debug.Log("Conservation Start");

        DialogSystem.instance.OpenDialogUI();
        DialogSystem.instance.dialogText.text = "Hello, How Can I help you?";
        DialogSystem.instance.option1BTN.transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>().text = "Bye";
        DialogSystem.instance.option1BTN.onClick.AddListener(() =>
        {
            DialogSystem.instance.CloseDialogUI();
            isTalkingWithPlayer = false;
        });
    }
}
