using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StaminaBoard : MonoBehaviour
{
    private Slider slider;
    public TextMeshProUGUI StaminaText;

    public GameObject playerState;

    private float currentStamina, maxStamina;
    // Start is called before the first frame update
    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get the value from PlayerState Script
        currentStamina = playerState.GetComponent<PlayerState>().currentStamina;
        maxStamina = playerState.GetComponent<PlayerState>().maxStamina;

        // to fill HP board follow the percentage in the HP board
        float fillValue = currentStamina / maxStamina;
        slider.value = fillValue;

        // display the currentHP on the left and maxHP on the right
        StaminaText.text = currentStamina + " / " + maxStamina;
    }
}
