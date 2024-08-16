using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpText : MonoBehaviour
{
    public Text hurtText;
    private Color colorNum;

    public float moveUpSpeed;

    public float speedWeak;


    public float fadeTime;

    private void Start()
    {
        hurtText = GetComponentInChildren<Text>();
        colorNum = hurtText.color;
        Init();
    }

    void Init()
    {
        moveUpSpeed = 1f;

        speedWeak = 0.1f;

        fadeTime = 3f;


       
        //hurtText.text = "LevelUp!";

        //hurtText.text = randomHurt.ToString();

    }


    private void Update()
    {
        transform.Translate(Vector3.up * moveUpSpeed * Time.deltaTime);

        if (moveUpSpeed > 0)
        {
            moveUpSpeed -= speedWeak;
        }
        else if (moveUpSpeed <= 0)
        {
            moveUpSpeed = 0;
        }

        if (moveUpSpeed == 0 && fadeTime > 0)
        {
            fadeTime -= Time.deltaTime;

            colorNum.a -= Time.deltaTime;

        }

        hurtText.color = colorNum;

        if (hurtText.color.a <= 0)
        {
            GameObject.Destroy(gameObject);
        }

    }


}
