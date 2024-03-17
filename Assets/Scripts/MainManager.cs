using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text currentUserText;

    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;

    private bool m_save = false;
    private bool isReplay = true;

    public Text Username;

    private bool m_GameOver = false;
    

    // Start is called before the first frame update
    void Start()
    {
        DataManagement.instance.LoadData();
        currentUserText.text = currentUserText.text + DataManagement.instance.Username;
        Username.text = DataManagement.instance.bestScore[0] != "" ? "Best Score : " + 
            DataManagement.instance.bestScore[0].Substring(3) : "Best Score : ";
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (isReplay)
            {
                isReplay = false;
                for (int i = 0; i < DataManagement.instance.bestScore.Count; i++)
                {
                    string temp = "";
                    for (int j = DataManagement.instance.bestScore[i].Length; j > 0; j--)
                    {
                        try
                        {
                            temp = int.Parse(DataManagement.instance.bestScore[i].Substring(j - 1)).ToString();
                        }
                        catch (System.Exception)
                        {
                            break;
                        }

                    }

                    if (temp != "" && m_Points > int.Parse(temp))
                    {
                        m_save = true;
                        DataManagement.instance.currentScore = m_Points;
                        break;
                    }

                }
                if (m_Points > DataManagement.instance.BestScore)
                {
                    m_save = true;
                    DataManagement.instance.BestScore = m_Points;
                    Username.text = "Best Score : " + DataManagement.instance.Username + $" : {DataManagement.instance.BestScore}";
                }
                if (m_save)
                {
                    m_save = false;
                    DataManagement.instance.BestScoreSaveData();
                }
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isReplay = true;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
    }

}
