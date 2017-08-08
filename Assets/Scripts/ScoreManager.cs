using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ScoreManager : MonoSingleton<ScoreManager>
{
    public int score;
    public int highScore;

    public Text scoreText;
    public Text highScoreText;

    public float scoreTimer;

    public GameManager.SceneMode sceneMode;

    public List<Chunk> chunkList;

    void Start()
    {
        sceneMode = GameManager.instance.sceneMode;
        switch(sceneMode)
        {
            case GameManager.SceneMode.MainMenu:
                highScoreText = GameObject.Find("High Score Counter").GetComponent<Text>();
                break;
            case GameManager.SceneMode.InGame:
                score = 0;
                scoreText = GameObject.Find("Score Counter").GetComponent<Text>();
                highScoreText = GameObject.Find("High Score Counter").GetComponent<Text>();
                break;
            default:
                break;
        }

        highScore = PlayerPrefs.GetInt("High Score");
        highScoreText.text = highScore.ToString();   
    }

    public void Update()
    {
        if(sceneMode == GameManager.SceneMode.InGame)
        {
            if (!GameManager.instance.gameOver)
            {
                scoreTimer += LevelManager.instance.levelSpeed * Time.deltaTime;
                if (scoreTimer / LevelManager.instance.levelSpeed > 1)
                {
                    scoreTimer = 0;
                    AddScore(1);
                }
            }
        }
    }

    public void AddScore(int amount)
    {                
        score += amount;
        UpdateScore();
    }

    public void UpdateScore()
    {
        scoreText.text = score.ToString() + "m";
        if (score >= highScore)
        {
            highScore = score;
            highScoreText.text = highScore.ToString() + "m";
        }
    }

    public void SaveHighScore()
    {
        if (score >= highScore)
        {
            PlayerPrefs.SetInt("High Score", highScore);
        }        
    }
}
