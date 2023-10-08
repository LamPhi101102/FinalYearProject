using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;
using StarterAssets;
using UnityEngine.UI;

public class InGameMenu : MonoBehaviour
{
    public ThirdPersonController thirdPersonController;

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}