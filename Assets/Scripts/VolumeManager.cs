using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider SFXSlider;
    [SerializeField] private AudioMixer mixer;

    void Start ()
    {
    if(PlayerPrefs.HasKey("MasterVolume"))
    {
        mixer.SetFloat("MasterVolume", PlayerPrefs.GetFloat("MasterVolume"));
        mixer.SetFloat("SFXVolume", PlayerPrefs.GetFloat("SFXVolume"));
        mixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("MusicVolume"));
    }
    SetSliders();
}
    void SetSliders ()
    {
        masterSlider.value = PlayerPrefs.GetFloat("MasterVolume");
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
    }

    public void MasterSlider()
    {
        mixer.SetFloat("MasterVolume", masterSlider.value);
        PlayerPrefs.SetFloat("MasterVolume", masterSlider.value);
    }

    public void MusicSlider()
    {
        mixer.SetFloat("MusicVolume", musicSlider.value);
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
    }

    public void SfxSlider()
    {
        mixer.SetFloat("SFXVolume", SFXSlider.value);
        PlayerPrefs.SetFloat("SFXVolume", SFXSlider.value);
    }
}
