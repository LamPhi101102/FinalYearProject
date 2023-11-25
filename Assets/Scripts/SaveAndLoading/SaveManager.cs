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

    // Json Project Save Path
    string jsonPathProject;
    // Json External/Real Save Path
    string jsonPathPersistant;
    // Binary Save Path
    string binaryPath;

    public bool isSavingToJson;

    private void Start()
    {      
        jsonPathProject = Application.dataPath + Path.AltDirectorySeparatorChar + "SaveGame.json";
        jsonPathPersistant = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "SaveGame.json";
        binaryPath = Application.persistentDataPath + "/save_game.bin";
    }

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
        if (isSavingToJson == true)
        {
            SaveGameDataToJsonFile(gameData);
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
            AllGameData gameData = LoadGameDataFromJsonFile();
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
    #endregion

    #region || ------- To Json Section=====||

    public void SaveGameDataToJsonFile(AllGameData gameData)
    {
        string json = JsonUtility.ToJson(gameData);

        string encrypted = EncryptionDecryption(json);

        using (StreamWriter writer = new StreamWriter(jsonPathProject))
        {
            writer.Write(encrypted);
            Debug.Log("Saved Game to Json file at" + jsonPathProject);
        };
    }

    public AllGameData LoadGameDataFromJsonFile()
    {
        using (StreamReader reader = new StreamReader(jsonPathProject))
        {
            string json = reader.ReadToEnd();

            string decrypted = EncryptionDecryption(json);

            AllGameData data = JsonUtility.FromJson<AllGameData>(decrypted);
            return data;
        };
    }

    #endregion

    #region || ------- To Binary Section=====||

    public void SaveGameDataToBinaryFile(AllGameData gameData)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        FileStream stream = new FileStream(binaryPath, FileMode.Create);

        formatter.Serialize(stream, gameData);
        stream.Close();
        Debug.Log("Data saved to" + binaryPath);


    }

    public AllGameData LoadGameDataFromBinaryFile()
    {
       
        if (File.Exists(binaryPath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(binaryPath, FileMode.Open);

            AllGameData data = formatter.Deserialize(stream) as AllGameData;
            stream.Close();

            Debug.Log("Game Loaded from" + binaryPath);

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


    #region || ======= Encrryption ====== ||

    public string EncryptionDecryption(string jsonString)
    {
        string keyword = "1234567";

        string result = "";

        for(int i = 0; i < jsonString.Length; i++)
        {
            result += (char)(jsonString[i] ^ keyword[i % keyword.Length]);
        }
        return result; // Encrypted or Decrypted String

        // XOR = "is there a difference"

        // ---- Encrypt----
        // Mike - 01101101 01101001 01101011 01100101
        // M -          01101101
        // Key -        00000001
        //
        // Encrypted  - 01101100

        // ---- Decrypt----
        // Encrypted  - 01101100
        // Key -        00000001
        //
        // M -          01101101



    }
    #endregion
}
