using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ToMenu : MonoBehaviour
{
    void Start()
    {
  
        this.GetComponent<Button>().onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        GameManager.isGameOver = false;
        SceneManager.LoadScene(0);
    }
}
