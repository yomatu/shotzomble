using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMController : MonoBehaviour
{


    void Update()
    {
        if (GameManager.isGameOver==true)
        {

            GetComponent<AudioSource>().Stop();
        }
    }
}
