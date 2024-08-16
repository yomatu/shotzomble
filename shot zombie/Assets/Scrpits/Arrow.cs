using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Rigidbody2D rb;
    bool hasHit;
   
    public bool target;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (hasHit==false)
        {
            
            float angle = Mathf.Atan2(rb.velocity.y,rb.velocity.x)*Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle,Vector3.forward);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        hasHit = true;
        rb.velocity = Vector2.zero;
        rb.isKinematic = true ;

       // 如果碰撞的物体的 tag 是 Enemy 或 Wall
         if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Explosion" || collision.gameObject.tag == "Arrow")
            {

            target = false;
            // 销毁箭矢
            Destroy(gameObject);

           
            }
        
    }



}
