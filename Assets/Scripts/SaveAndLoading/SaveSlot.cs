using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;
using System;
using Debug = UnityEngine.Debug;

public class SaveSlot : MonoBehaviour
{
    private Button button;
    private TextMeshProUGUI buttonText;
    public int slotNumber;

    public GameObject alertUI;
    Button YesBTN;
    Button NoBTN;


    private void Awake()
    {
        button = GetComponent<Button>();
        buttonText = button.transform.Find("SaveSlotText").GetComponent<TextMeshProUGUI>();

        YesBTN = alertUI.transform.Find("YesButton").GetComponent<Button>();
        NoBTN = alertUI.transform.Find("NoButton").GetComponent<Button>();
    }

    public void Start()
    {
        button.onClick.AddListener(() =>
        {
            if (SaveManager.instance.isSlotEmpty(slotNumber))
            {
                SaveGameConfirmed();
            }
            else
            {
                DisplayOverrideWarning();
            }
        });
    }
 

    public void DisplayOverrideWarning()
    {
        alertUI.SetActive(true);

        YesBTN.onClick.AddListener(() =>
        {
            SaveGameConfirmed();
            alertUI.SetActive(false);
        });

        NoBTN.onClick.AddListener(() =>
        {
            alertUI.SetActive(false);
        });
    }

    private void SaveGameConfirmed()
    {
        SaveManager.instance.SaveGame(slotNumber);

        DateTime dt = DateTime.Now;
        string time = dt.ToString("yyyy-MM-dd HH:mm");

        string description = "Saved Game " + slotNumber + " | " + time;

        buttonText.text = description;

        PlayerPrefs.SetString("Slot" + slotNumber + "Desctiption", description);

        buttonText.text = PlayerPrefs.GetString("Slot" + slotNumber + "Desctiption");
        SaveManager.instance.DeselectButton();
    }

    private void Update()
    {
        if (SaveManager.instance.isSlotEmpty(slotNumber))
        {
            buttonText.text = "Empty";
        }
        else
        {
            buttonText.text = PlayerPrefs.GetString("Slot" + slotNumber + "Desctiption");
        }
    }

}
