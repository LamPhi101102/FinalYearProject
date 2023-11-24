using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CaloriesBoard : MonoBehaviour
{
    private Slider slider;
    public TextMeshProUGUI CaloBoard;

    public GameObject playerState;

    private float currentCaloriesPercent, maxCaloriesPercent;
    // Start is called before the first frame update
    void Awake()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerState == null)
        {
            return;
        }
        // Get the value from PlayerState Script
        currentCaloriesPercent = playerState.GetComponent<PlayerState>().currentCaloriesPercent;
        maxCaloriesPercent = playerState.GetComponent<PlayerState>().maxCaloriesPercent;

        // to fill HP board follow the percentage in the HP board
        float fillValue = currentCaloriesPercent / maxCaloriesPercent;
        slider.value = fillValue;

        // display the currentHP on the left and maxHP on the right
        CaloBoard.text = currentCaloriesPercent + " / " + maxCaloriesPercent;
    }
}
