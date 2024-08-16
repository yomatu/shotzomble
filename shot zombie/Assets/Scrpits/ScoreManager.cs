using UnityEngine.UI;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;
    public int score;
    private int highScore;
    private string highScoreKey = "HighScore";

    
    void Start()
    {
        score = 0;  

        highScore = PlayerPrefs.GetInt(highScoreKey,0);
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        scoreText.text = score + " Kill";
    }

    public void AddScore(int value)
    {
        score+=value;

        GameManager.playerScore = score;

        if (score>highScore)
        {
            highScore = score;

            PlayerPrefs.SetInt(highScoreKey,highScore);
        }

        UpdateScoreText();
    }

}
