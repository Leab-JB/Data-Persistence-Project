using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManagement : MonoBehaviour
{
    // Singleton
    public static DataManagement instance;

    public string Username;
    public int currentScore;
    public int BestScore;
    public List<string> bestScore;

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
        public List<string> bestScore;
    }

    public void BestScoreSaveData()
    {
        SaveData save = new();
        save.Username = Username;
        save.BestScore = BestScore;
        save.bestScore = bestScore;

        for (int i = 0; i < 5; i++)
        {
            if (bestScore[i] != "")
            {
                // getting the score and removing the text
                string temp = "";
                for (int j = bestScore[i].Length; j > 0; j--)
                {
                    try
                    {
                        temp = int.Parse(bestScore[i].Substring(j-1)).ToString();
                    }
                    catch (System.Exception)
                    {
                        break;
                    }
                    
                }

                // checking if the current score is greater than an internal score
                if (currentScore > int.Parse(temp))
                {
                    save.bestScore.Insert(i, " - " + Username + " : " + currentScore);
                    
                    // remove last element
                    save.bestScore.RemoveAt(save.bestScore.Count - 1);
                    break;
                }
            }
            // add the score if the next element in the list is empty
            else if (bestScore[i] == "")
            {
                save.bestScore[i] = " - " + Username + " : " + BestScore;
                break;
            }
        }

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

            //Username = data.Username;
            BestScore = data.BestScore;

            bestScore = data.bestScore;
            return;
        }
        bestScore = new()
        {
            "",
            "",
            "",
            "",
            ""
        };
    }

    public void DeleteData()
    {
        string path = Application.persistentDataPath + "/saveGame.json";
        if (File.Exists(path))
        {
            File.Delete(path);
            Username = null;
            BestScore = 0;
            bestScore = new()
            {
                "",
                "",
                "",
                "",
                ""
            };
        }
    }
}
