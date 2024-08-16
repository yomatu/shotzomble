using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AimWithMouse : MonoBehaviour
{

    public Image Aim; 
  

    void Update()
    {
   
        Vector3 AimPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        AimPosition.z = 0;

        
       Aim.transform.position = AimPosition;
    }
}
