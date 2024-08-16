using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
 

    public GameObject gameOverPanel;

    public static bool isGameOver=false;

    public Text gameOverTime;

    public Text gameOverKill;

    public static int playerScore;


    public float gameWatch;
    //if over this time is gameover
    public float timeLimit;
    public Text watchText;
    public int hasKilled;


    private void Awake()
    {
        gameWatch = 60;
        timeLimit = 0;
    }

    private void Update()
    {
        UpdateTime();
        //gameover  
        if (!isGameOver )
        {
            Time.timeScale = 1;

        }

        if (isGameOver)
        {
            gameOverTime.text = "残り時間:"+ Mathf.FloorToInt(gameWatch % 60).ToString();

            gameOverKill.text = "キル数:" + playerScore.ToString();

            gameOverPanel.SetActive(true);
           // Debug.Log("isGameOver");

            Time.timeScale = 0;
        }


    }




    private void UpdateTime()
    {
        if (gameWatch > 0)
        {
            gameWatch -= Time.deltaTime;
        }
        // 如果gameWatch小于0，就让它等于0，避免出现负数
        else
        {
            gameWatch = 0;
        }
     
        UpdateWatchUI();

        if (gameWatch <= timeLimit)
        {
            isGameOver = true;
        }
        
    }

    private void UpdateWatchUI()
    {
        int minutes = Mathf.FloorToInt(gameWatch / 60);

        int seconds = Mathf.FloorToInt(gameWatch % 60);

        string text = string.Format("{0:00}:{1:00}", minutes, seconds);

        watchText.text = text;
    }


  

}
