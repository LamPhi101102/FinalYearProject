using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance { get; set; }

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
    private void Start()
    {
        DayUI.text = $"{dayInGame} Days";
    }

    public int dayInGame = 1;

    public TextMeshProUGUI DayUI;

    public void TriggerNextDay()
    {
        dayInGame += 1;
        DayUI.text = $"{dayInGame} Days";
    }
}
