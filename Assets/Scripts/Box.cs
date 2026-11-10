using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] ParticleSystem particleSystem;
    [SerializeField] AudioSource boxAudioSource;
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
        boxAudioSource.pitch = Random.Range(0.5f, 3f);
        boxAudioSource.Play();
    }
}
