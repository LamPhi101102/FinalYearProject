using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine;
using Debug = UnityEngine.Debug;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{ 
    public static SaveManager instance { get; set; }
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

        DontDestroyOnLoad(gameObject);
    }

    public bool isSavingToJson;

    #region || ------- To General Section=====||

    #region || ------- Saving =====||

    public void SaveGame()
    {
        AllGameData data = new AllGameData();
        data.playerData = GetPlayerData();

        SavingTypeSwitch(data);
    }

    private PlayerData GetPlayerData()
    {
        float[] playerStats = new float[3];
        playerStats[0] = PlayerState.Instance.currentHP;
        playerStats[1] = PlayerState.Instance.currentStamina;
        playerStats[2] = PlayerState.Instance.currentCaloriesPercent;

        float[] playerPosAndRot = new float[6];
        playerPosAndRot[0] = PlayerState.Instance.playerBody.transform.position.x;
        playerPosAndRot[1] = PlayerState.Instance.playerBody.transform.position.y;
        playerPosAndRot[2] = PlayerState.Instance.playerBody.transform.position.z;

        playerPosAndRot[3] = PlayerState.Instance.playerBody.transform.rotation.x;
        playerPosAndRot[4] = PlayerState.Instance.playerBody.transform.rotation.y;
        playerPosAndRot[5] = PlayerState.Instance.playerBody.transform.rotation.z;
    
        return new PlayerData(playerStats, playerPosAndRot);
    }


    public void SavingTypeSwitch(AllGameData gameData)
    {
        if (isSavingToJson)
        {
            // SaveGameDataToJsonFile(gameData);
        }
        else
        {
            SaveGameDataToBinaryFile(gameData);
        }
    }
    #endregion

    #region || ------- Loading =====||

    public AllGameData LoadingTypeSwitch()
    {
        if (isSavingToJson)
        {
            AllGameData gameData = LoadGameDataFromBinaryFile();
            return gameData;
        }
        else
        {
            AllGameData gameData = LoadGameDataFromBinaryFile();
            return gameData;
        }
    }

    public void LoadGame()
    {
        //Player Data
        SetPlayerData(LoadingTypeSwitch().playerData);
        
        // Environment Data

    }

    private void SetPlayerData(PlayerData playerData)
    {
        // Setting Player Stats
        PlayerState.Instance.currentHP = playerData.playerStats[0];
        PlayerState.Instance.currentStamina = playerData.playerStats[1];
        PlayerState.Instance.currentCaloriesPercent = playerData.playerStats[2];

        // Settings Player Position
        Vector3 loadedPosition;
        loadedPosition.x = playerData.playerPositionandRotation[0];
        loadedPosition.y = playerData.playerPositionandRotation[1];
        loadedPosition.z = playerData.playerPositionandRotation[2];

        PlayerState.Instance.playerBody.transform.position = loadedPosition;

        // Setting Player rotation
        Vector3 loadedRotation;
        loadedRotation.x = playerData.playerPositionandRotation[3];
        loadedRotation.y = playerData.playerPositionandRotation[4];
        loadedRotation.z = playerData.playerPositionandRotation[5];

        PlayerState.Instance.playerBody.transform.rotation = Quaternion.Euler(loadedRotation);

    }
    
    public void StartLoadedGame()
    {
        SceneManager.LoadScene("GameScene");

        StartCoroutine(DelayedLoading());
    }

    private IEnumerator DelayedLoading()
    {
        yield return new WaitForSeconds(1f);
        LoadGame();     
    }

    #endregion

    #region || ------- To Binary Section=====||

    public void SaveGameDataToBinaryFile(AllGameData gameData)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/save_game.bin";
        FileStream stream = new FileStream(path, FileMode.Create);

        formatter.Serialize(stream, gameData);
        stream.Close();
        Debug.Log("Data saved to" + Application.persistentDataPath + "/save_game.bin");


    }

    public AllGameData LoadGameDataFromBinaryFile()
    {
        string path = Application.persistentDataPath + "/save_game.bin";
        
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            AllGameData data = formatter.Deserialize(stream) as AllGameData;
            stream.Close();

            Debug.Log("Game Loaded from" + Application.persistentDataPath + "/save_game.bin");

            return data;
        }
        else
        {
            return null;
        }
    }


    #endregion

    #region || ------- Volume Section=====||
    [System.Serializable]
    public class VolumeSettings
    {
        public float music;
        public float effects;
        public float master;
    }

    public void SaveVolumeSettings(float _music, float _effects, float _master) 
    {
        VolumeSettings volumeSettings = new VolumeSettings()
        {
            music = _music,
            effects = _effects,
            master = _master
        };

        PlayerPrefs.SetString("Volume", JsonUtility.ToJson(volumeSettings));
        PlayerPrefs.Save();
    }

    public VolumeSettings LoadVolumeSettings()
    {
        return JsonUtility.FromJson<VolumeSettings>(PlayerPrefs.GetString("Volume"));    
    }
    public float LoadMusicVolume()
    {
        var volumeSettings = JsonUtility.FromJson<VolumeSettings>(PlayerPrefs.GetString("Volume"));
        return volumeSettings.music;
    }
    #endregion

    #endregion
}
