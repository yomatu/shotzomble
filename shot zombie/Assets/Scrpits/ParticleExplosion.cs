using UnityEngine;

public class ParticleExplosion : MonoBehaviour
{
    public GameObject explosion; 

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")|| collision.gameObject.CompareTag("Wall")) // 假??人???"Enemy"
        {

            GameObject go = Instantiate(explosion, transform.position, transform.rotation);



            Destroy(gameObject);
        }
    }
}