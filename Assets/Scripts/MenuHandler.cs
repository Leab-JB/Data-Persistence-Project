using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEditor;
using TMPro;

public class MenuHandler : MonoBehaviour
{
    public TMP_InputField inputText;
    private void Start()
    {
        if (inputText != null && !string.IsNullOrEmpty(DataManagement.instance.Username))
        {
            inputText.text = DataManagement.instance.Username;
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

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
