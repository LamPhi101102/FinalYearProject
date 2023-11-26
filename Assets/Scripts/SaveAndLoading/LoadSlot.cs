using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadSlot : MonoBehaviour
{
    public Button button;
    public TextMeshProUGUI buttonText;

    public int slotNumber;

    private void Awake()
    {
        button = GetComponent<Button>();
        buttonText = button.transform.Find("LoadSlotText").GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (SaveManager.instance.isSlotEmpty(slotNumber))
        {
            buttonText.text = "";
        }
        else
        {
            buttonText.text = PlayerPrefs.GetString("Slot" + slotNumber + "Desctiption");
        }
    }
    private void Start()
    {
        button.onClick.AddListener(() =>
        {
            if(SaveManager.instance.isSlotEmpty(slotNumber) == false)
            {
                SaveManager.instance.StartLoadedGame(slotNumber);
                //SaveManager.instance.DeselectButton();
            }
            else
            {

            }
        });
    }
}
