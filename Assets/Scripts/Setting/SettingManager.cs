using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = UnityEngine.Debug;
using UnityEngine.UI;
using TMPro;
using static SaveManager;

public class SettingManager : MonoBehaviour
{
    public static SettingManager instance { get; set; }
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

    public Button backBTN;

    public Slider masterSlider;
    public GameObject mastervalue;

    public Slider musicSlider;
    public GameObject musicvalue;

    public Slider effectsSlider;
    public GameObject effectsvalue;

    private void Start()
    {
        backBTN.onClick.AddListener(() =>
        {
            SaveManager.instance.SaveVolumeSettings(musicSlider.value, effectsSlider.value, masterSlider.value);
            Debug.Log("Saved");
        });

        StartCoroutine(LoadAndApplySettings());
    }



    private IEnumerator LoadAndApplySettings()
    {
        LoadAndSetVolume();
        // Load GraphicsSettings
        // Load Key bindings

        yield return new WaitForSeconds(0.1f);
    }

    private void LoadAndSetVolume()
    {
        VolumeSettings volumeSettings = SaveManager.instance.LoadVolumeSettings();
        
        masterSlider.value = volumeSettings.master;
        musicSlider.value = volumeSettings.music;
        effectsSlider.value = volumeSettings.effects;

        Debug.Log("Volume settings are Loaded");
    }






    private void Update()
    {
        mastervalue.GetComponent<TextMeshProUGUI>().text = "" + (masterSlider.value) + "";
        musicvalue.GetComponent<TextMeshProUGUI>().text = "" + (musicSlider.value) + "";
        effectsvalue.GetComponent<TextMeshProUGUI>().text = "" + (effectsSlider.value) + "";
    }
}
