using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBoard : MonoBehaviour
{
    private Slider slider;
    public TextMeshProUGUI HPBoard;


    public GameObject playerState;

    private float currentHP, maxHP;
    // Start is called before the first frame update
    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get the value from PlayerState Script
        currentHP = playerState.GetComponent<PlayerState>().currentHP;
        maxHP = playerState.GetComponent<PlayerState>().maxHP;

        // to fill HP board follow the percentage in the HP board
        float fillValue = currentHP / maxHP;
        slider.value = fillValue;

        // display the currentHP on the left and maxHP on the right
        HPBoard.text = currentHP + " / " + maxHP;     
    }
}
