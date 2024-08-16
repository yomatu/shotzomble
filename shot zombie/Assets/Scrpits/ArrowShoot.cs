using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class ArrowShoot : MonoBehaviour
{
  
    public GameObject arrowAndBoom;

    public GameObject isChangeBoomArrowButton;

    public bool isUseedBoomArrow=false;

    public bool isBoomArrow=false;

    public GameObject boomReadyText;


    public GameObject levelUpText;

    public int level = 1; //定义当前等级

    public int maxLevel = 3; //定义最大等级

    public int arrowsPerLevel = 1; //定义每个等级对应的箭矢数量

    public float angleOffset = 10f; //定义每个箭矢之间的夹角

    private ScoreManager scoreManager; //定义ScoreManager组件


    public CinemachineVirtualCamera vcam;

    public Arrow currentArrow;

    public GameObject arrow;
    public Transform shotPoint;

    public GameObject point;
    GameObject[] points;
    public int numberOfPoints;
    public float spaceBetweenPoints;
    Vector2 direction;

    float pressTime; //记录按键时间
    float launchForce; //定义发射力度
    const float MAX_LAUNCH_FORCE = 25f; //定义最大发射力度

    public Transform target;

    public bool isShotting;

    private void Start()
    {
        target = GameObject.Find("Player").transform;

        vcam.Follow = target;

        isShotting = false;

        points = new GameObject[numberOfPoints];
        for (int i = 0; i < numberOfPoints; i++)
        {
            points[i] = Instantiate(point, shotPoint.position, Quaternion.identity);
        }

        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>(); //获取ScoreManager组件


    }

    void Update()
    {
        ChangeArrow();

        if (currentArrow != null && currentArrow.target)
        {
            // 如果currentArrow不为空且target为true，就跟随currentArrow
            vcam.Follow = currentArrow.transform;
        }
        else
        {
            // 否则就跟随玩家
            vcam.Follow = target;
        }
        Vector2 bowPosition = transform.position;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = mousePosition - bowPosition;
        transform.right = direction;

        if (Input.GetMouseButtonDown(0))
        {
            pressTime = Time.time; //按下时记录时间
        }
        else if (Input.GetMouseButton(0)) //按住时更新发射力度和轨迹
        {
            float holdTime = Time.time - pressTime; //计算按键时长
            launchForce = Mathf.Min(holdTime * 50f, MAX_LAUNCH_FORCE); //根据时长计算发射力度，不超过最大值
            for (int i = 0; i < numberOfPoints; i++)
            {
                points[i].transform.position = PointPosition(i * spaceBetweenPoints, launchForce); //更新轨迹
                points[i].SetActive(true);
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            float holdTime = Time.time - pressTime; //松开时计算按键时长
            launchForce = Mathf.Min(holdTime * 50f, MAX_LAUNCH_FORCE); //根据时长计算发射力度，不超过最大值
            Shoot(launchForce); //发射箭矢

            isShotting = true;

            if (isBoomArrow == true)
            {
                isBoomArrow = false;
            }

            for (int i = 0; i < numberOfPoints; i++)
            {
                points[i].SetActive(false); // 禁用 points[i]
            }

        }

        CheckScore(); //检查分数并更新等级和箭矢数量

    }
    void Shoot(float launchForce) //传入发射力度
    {
        if (isBoomArrow == false)
        {
            //修改为循环发射多个箭矢
            for (int i = 0; i < arrowsPerLevel; i++)
            {
                GameObject newArrow = Instantiate(arrow, shotPoint.position, shotPoint.rotation); //实例化箭矢
                newArrow.transform.Rotate(0, 0, angleOffset * (i - (arrowsPerLevel - 1) / 2f)); //根据夹角旋转箭矢
                newArrow.GetComponent<Rigidbody2D>().velocity = newArrow.transform.right * launchForce; //根据发射力度和箭矢方向设置速度
                if (i == 0) //如果是第一个箭矢，就赋值给currentArrow
                {
                    currentArrow = newArrow.GetComponent<Arrow>();
                    currentArrow.target = true;
                }
            }
        }
        else if (isBoomArrow == true)
        {
            for (int i = 0; i < 1; i++)
            {
                GameObject newArrow = Instantiate(arrowAndBoom, shotPoint.position, shotPoint.rotation); //实例化箭矢
                newArrow.transform.Rotate(0, 0, angleOffset * (i - (arrowsPerLevel - 1) / 2f)); //根据夹角旋转箭矢
                newArrow.GetComponent<Rigidbody2D>().velocity = newArrow.transform.right * launchForce; //根据发射力度和箭矢方向设置速度
                if (i == 0) //如果是第一个箭矢，就赋值给currentArrow
                {
                    currentArrow = newArrow.GetComponent<Arrow>();
                    currentArrow.target = true;
                }
            }

        } 

    }


    Vector2 PointPosition(float t, float launchForce) //传入发射力度
    {
        Vector2 position = (Vector2)shotPoint.position +
            (direction.normalized * launchForce * t) + 0.5f * Physics2D.gravity * (t * t); //根据发射力度计算位置
        return position;
    }

    void CheckScore() //检查分数并更新等级和箭矢数量
    {
        int score = scoreManager.score; //获取当前分数
        int newLevel = score / 5 + 1; //根据分数计算新的等级
        if (newLevel > maxLevel) //如果新的等级超过最大等级，就保持最大等级
        {
            newLevel = maxLevel;
        }
        if (newLevel != level) //如果新的等级和当前等级不同，就更新等级和箭矢数量
        {
            level = newLevel;
            arrowsPerLevel = level;
            GameObject levelUpTextInstance = Instantiate(levelUpText, transform.position+Vector3.up*1.2f, transform.rotation);

            //levelUpTextInstance.transform.SetParent(FindObjectOfType<Canvas>().transform);

            //// 设置预制体的位置和旋转为相对于Canvas的值
            //levelUpTextInstance.transform.localPosition = Vector3.zero;
            //levelUpTextInstance.transform.localRotation = Quaternion.identity;
        }
    }

    public void ChangeArrow()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isUseedBoomArrow==false)
            {
                isBoomArrow = true;
                Debug.Log("isBoomArrow:" + isBoomArrow);

                isChangeBoomArrowButton.SetActive(false);
                isUseedBoomArrow = true;
                GameObject levelUpTextInstance = Instantiate(boomReadyText, transform.position + Vector3.up * 1.2f, transform.rotation);
            }
            else
            {
                return;
            }
        }
  
    }

}
