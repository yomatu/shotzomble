using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
   // public GameObject gameOverPanel;

  //  public bool isGameOver=false;


    private Transform target;
    private Animator myAnimator; // 敌人的动画控制器
    private bool isDead = false; // 是否已经死亡

    public float moveSpeed;

    public int ScoreValue = 1;
    private ScoreManager scoreManager;

    void Start()
    {

      //  gameOverPanel = GameObject.Find("GameOverPanel");

        target = GameObject.FindGameObjectWithTag("Player").transform;
        myAnimator = GetComponent<Animator>(); // 获取敌人的动画控制器组件
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
    }

    void Update()
    {
        if (!isDead)
        {
            Move();
        }
    }

    private void Move()
    {
        //move to target
        if (target != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, Time.deltaTime * moveSpeed);
        }
        else
        {
            return;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Arrow"|| other.tag == "Explosion")
        {
            // 播放死亡音效
            GetComponent<AudioSource>().Play(); 

            // 设置为死亡状态
            isDead = true;

            // 播放死亡动画
            myAnimator.Play("ZombieDead");

            // 停用移动
            enabled = false;


            GetComponent<Collider2D>().enabled = false;


            scoreManager.AddScore(ScoreValue);

            // 延迟一段时间后销毁敌人对象
            StartCoroutine(DestroyEnemy());
        }
        if (other.tag == "Player")
        {
            GameManager.isGameOver = true;

            //   gameOverPanel.SetActive(true);
            Debug.Log("isGameOver");
            
           // Time.timeScale = 0;   
        }
    }

    private IEnumerator DestroyEnemy()
    {
        yield return new WaitForSeconds(1f); // 延迟1秒
        Destroy(gameObject); // 销毁敌人对象
    }
}