using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class DialogSystem : MonoBehaviour
{
    public static DialogSystem instance { get; set; }

    public TextMeshProUGUI dialogText;
    public Button option1BTN;
    public Button option2BTN;

    public Canvas dialogUI;

    public bool dialogUIActive;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void OpenDialogUI()
    {
        dialogUI.gameObject.SetActive(true);
        dialogUIActive = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CloseDialogUI()
    {
        dialogUI.gameObject.SetActive(false);
        dialogUIActive = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

}
