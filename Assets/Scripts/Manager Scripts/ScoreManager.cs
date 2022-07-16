using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    private static ScoreManager instance;

    private int score;

    private Text scoreText;

    void Awake()
    {
        MakeSingleton();
    }
    private void MakeSingleton()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }


    private void Start()
    {
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        AddScore(0);
    }


    private void Update()
    {
        if(scoreText == null)
        {
            scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
            scoreText.text = score.ToString();
        }


    }

    public void AddScore(int amount)
    {
        score += amount;
        if(score > PlayerPrefs.GetInt("HighScore",0)){
            PlayerPrefs.SetInt("HighScore", score);
        }

        scoreText.text = score.ToString();
    }

    public void ResetScore()
    {
        score = 0;
        AddScore(0);
    }

    #region GetterSetter
    public static ScoreManager Instance { get => instance; set => instance = value; }
    public int Score { get => score; set => score = value; }
    public int BestScore { get => PlayerPrefs.GetInt("HighScore",0);}
    #endregion


}
