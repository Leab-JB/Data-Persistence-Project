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
    private void Start()
    {
        DataManagement.instance.LoadData();
        if (inputText != null && !string.IsNullOrEmpty(DataManagement.instance.Username))
        {
            inputText.text = DataManagement.instance.Username;
        }
        if (bestScoreText != null)
        {
            bestScoreText.text = "Best Score : " + DataManagement.instance.Username + " : " +
                DataManagement.instance.BestScore.ToString();
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
