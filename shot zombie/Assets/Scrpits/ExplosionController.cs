using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    private ParticleSystem explosionParticles; 

    void Start()
    {
        explosionParticles = GetComponent<ParticleSystem>();

    }

    void Update()
    {
 
        if (!explosionParticles.isPlaying)
        {
            Destroy(gameObject); 
        }
    }
}
