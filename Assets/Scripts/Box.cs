using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] ParticleSystem particleSystem;
    [SerializeField] GameObject box;
    void OnCollisionEnter2D(Collision2D collision)
    {
        particleSystem.transform.LookAt(collision.transform);
        emitParticle();
        emitSound();
    }

    void emitParticle()
    {
        particleSystem.Emit(10);
    }

    void emitSound()
    {
        
    }
}
