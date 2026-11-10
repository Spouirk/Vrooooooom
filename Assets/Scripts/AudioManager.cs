using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] private AudioSource buttonAudioSource;
    [SerializeField] private AudioSource muteCity;
    [SerializeField] private AudioSource victoryAudioSource;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
    }
    
    public void PlayButton()
    {
        buttonAudioSource.Play();
    }

    public void PlayMuteCity()
    {
        muteCity.Play();
    }

    public void StopMuteCity()
    {
        muteCity.Stop();
    }

    public void PlayVictory()
    {
        victoryAudioSource.Play();
    }

    public void StopVictory()
    {
        victoryAudioSource.Stop();
    }
}
