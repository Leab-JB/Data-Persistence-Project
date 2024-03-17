using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEditor;
using TMPro;

public class MenuHandler : MonoBehaviour
{
    public TMP_InputField inputText;
    public TMP_Text bestScoreText;

    public GameObject top5Scores;
    public Transform parent;

    private void Start()
    {
        DataManagement.instance.LoadData();

        if (inputText != null && DataManagement.instance.Username != null){
            inputText.text = DataManagement.instance.Username;
        }

        // instantiating the scoreboard data if not empty
        if (top5Scores != null)
        {
            int i = 1;
            foreach (var score in DataManagement.instance.bestScore)
            {
                if (score != "")
                {
                    GameObject obj = Instantiate(top5Scores, parent);
                    obj.GetComponentInChildren<TMP_Text>().text = i + score;
                    i++;
                }
            }
        }
        
        if (bestScoreText != null)
        {
            
            if(DataManagement.instance.bestScore[0] != "")
            {
                bestScoreText.text = "Best Score : " + DataManagement.instance.bestScore[0].Substring(3);
            }
            else
            {
                bestScoreText.text = "Best Score :: ";
            }
        }
    }
    public void StartGame()
    {
        DataManagement.instance.Username = inputText.text;
        SceneManager.LoadScene(1);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void DeleteData()
    {
        DataManagement.instance.DeleteData();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
