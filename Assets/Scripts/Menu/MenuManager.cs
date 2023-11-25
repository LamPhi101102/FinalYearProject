using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.UI;
using StarterAssets;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance { get; set; }

    public GameObject menuCanvas;
    public GameObject uiCanvas;

    public GameObject ContinueMenu;
    public GameObject SaveGameMenu;
    public GameObject OptionMenu;

    public GameObject menu;

    public GameObject menuButton;

    Button ContinueButton;

    public bool isMenuOpen;

    void Start()
    {
        isMenuOpen = false;
        ContinueButton = menu.transform.Find("ContinueButton").GetComponent<Button>();
        ContinueButton.onClick.AddListener(delegate { CloseMenu(); });
    }
    public void CloseMenu()
    {
        menu.SetActive(true);
        SaveGameMenu.SetActive(false);
        OptionMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
        menuCanvas.SetActive(false);
        uiCanvas.SetActive(true);
        isMenuOpen = false;

        SelectionManager.Instance.EnableSelection();
        SelectionManager.Instance.GetComponent<SelectionManager>().enabled = true;
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M) && !isMenuOpen)
        {
            menuCanvas.SetActive(true);
            uiCanvas.SetActive(false);
            isMenuOpen = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SelectionManager.Instance.DisableSelection();
            SelectionManager.Instance.GetComponent<SelectionManager>().enabled = false;
        }
        else if (Input.GetKeyDown(KeyCode.M) && isMenuOpen)
        {
            menu.SetActive(true);
            SaveGameMenu.SetActive(false);
            OptionMenu.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = true;
            menuCanvas.SetActive(false);
            uiCanvas.SetActive(true);
            isMenuOpen = false;
            SelectionManager.Instance.EnableSelection();
            SelectionManager.Instance.GetComponent<SelectionManager>().enabled = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isMenuOpen)
        {
            menu.SetActive(true);
            SaveGameMenu.SetActive(false);
            OptionMenu.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = true;
            menuCanvas.SetActive(false);
            uiCanvas.SetActive(true);
            isMenuOpen = false;
            SelectionManager.Instance.EnableSelection();
            SelectionManager.Instance.GetComponent<SelectionManager>().enabled = true;
        }
    }

    public void TempSaveGame()
    {
        SaveManager.instance.SaveGame();
    }

}
