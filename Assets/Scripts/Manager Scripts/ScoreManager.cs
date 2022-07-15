using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    private static ScoreManager instance;

    private int score;


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
        AddScore(0);
    }




    public void AddScore(int amount)
    {
        score += amount;
        if(score > PlayerPrefs.GetInt("HighScore",0)){
            PlayerPrefs.SetInt("HighScore", score);
        }

        Debug.Log(score);
        //LoadTheText
    }

    public void ResetScore()
    {
        score = 0;
    }

    #region GetterSetter
    public static ScoreManager Instance { get => instance; set => instance = value; }
    public int Score { get => score; set => score = value; }
    #endregion


}
