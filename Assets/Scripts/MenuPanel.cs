using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuPanel : MonoBehaviour {

    Text highScoreCounter;
    Text scoreCounter;

    void GetScores()
    {
        highScoreCounter.text = ScoreManager.instance.highScore.ToString();
        scoreCounter.text = ScoreManager.instance.score.ToString();
    }
}
