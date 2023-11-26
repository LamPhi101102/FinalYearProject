using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float[] playerStats; // [0] - Health, [1] - Hydration, [2] Hydration
    
    public float[] playerPositionandRotation; // position x,y,z and rotaion x,y,z
    
    public string[] inventoryContent;

    public string[] quickSlotsContent;

    public PlayerData(float[] _playerStats, float[] _playerPosAndRot, string[] _inventoryContent, string[] _quickSlotsContent)
    {
        playerStats = _playerStats;
        playerPositionandRotation = _playerPosAndRot;
        inventoryContent = _inventoryContent;
        quickSlotsContent = _quickSlotsContent;
    }
}
