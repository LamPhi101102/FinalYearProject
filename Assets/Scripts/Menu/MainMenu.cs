using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

public class MainMenu : MonoBehaviour
{
    public Button LoadGameBTN;

    private void Start()
    {
        LoadGameBTN.onClick.AddListener(() =>
        {
            SaveManager.instance.StartLoadedGame();
        });
    }

    public void NewGame()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void ExitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
}
