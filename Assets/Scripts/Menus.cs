using UnityEngine;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{
    void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    
    public void OnPlayButton()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
        AudioManager.instance.PlayButton();
        AudioManager.instance.PlayMuteCity();
        AudioManager.instance.StopVictory();
    }

    public void OnQuitButton()
    {
        Application.Quit();     
        AudioManager.instance.PlayButton();   
    }

    public void OnMenuButton()
    {
        SceneManager.LoadScene(0);
        AudioManager.instance.PlayButton();
        AudioManager.instance.StopMuteCity();
        AudioManager.instance.StopVictory();
    }

    public void OnTutorialButton()
    {
        SceneManager.LoadScene(2);
        AudioManager.instance.PlayButton();
    }

    public void OnSettingsButton()
    {
        SceneManager.LoadScene(3);
        AudioManager.instance.PlayButton();
    }
}