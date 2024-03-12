using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManagement : MonoBehaviour
{
    // Singleton
    public static DataManagement instance;

    public string Username;
    public int BestScore;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    [System.Serializable]
    class SaveData
    {
        public string Username;
        public int BestScore;
    }

    public void SaveUserData()
    {
        SaveData save = new();
        save.Username = Username;
        save.BestScore = BestScore;

        string json = JsonUtility.ToJson(save);
        File.WriteAllText(Application.persistentDataPath + "/saveGame.json", json);
    }

    public void LoadData()
    {
        string path = Application.persistentDataPath + "/saveGame.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            Username = data.Username;
            BestScore = data.BestScore;

        }
    }

    public void DeleteData()
    {
        string path = Application.persistentDataPath + "/saveGame.json";
        if (File.Exists(path))
        {
            File.Delete(path);
            Username = null;
            BestScore = 0;
        }
    }
}
