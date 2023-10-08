using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemScript : MonoBehaviour
{
    public int count = 0;

    public void UpdateItemCountUI(TextMeshProUGUI uiText)
    {
        uiText.text = count.ToString();
    }
}
