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
    private float recoveryInterval = 10f; // Time interval for recovery
    private float recoveryAmount = 1f;    // Amount of stamina to recover

    private float nextRecoveryTime;
    // Start is called before the first frame update
    void Awake()
    {
        slider = GetComponent<Slider>();
        nextRecoveryTime = Time.time + recoveryInterval;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerState == null)
        {
            return;
        }
        // Get the value from PlayerState Script
        currentStamina = playerState.GetComponent<PlayerState>().currentStamina;
        maxStamina = playerState.GetComponent<PlayerState>().maxStamina;

        // to fill HP board follow the percentage in the HP board
        float fillValue = currentStamina / maxStamina;
        slider.value = fillValue;

        // display the currentHP on the left and maxHP on the right
        StaminaText.text = currentStamina + " / " + maxStamina;

        // Check for stamina recovery
        if (currentStamina < maxStamina && Time.time >= nextRecoveryTime)
        {
            // Recover 1 unit of stamina
            currentStamina += recoveryAmount;

            // Ensure currentStamina doesn't exceed maxStamina
            currentStamina = Mathf.Min(currentStamina, maxStamina);

            // Update the PlayerState with the new currentStamina value
            playerState.GetComponent<PlayerState>().currentStamina = currentStamina;

            // Set the next recovery time
            nextRecoveryTime = Time.time + recoveryInterval;
        }


    }
    
}
