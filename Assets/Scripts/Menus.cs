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
    }

    public void OnQuitButton()
    {
        Application.Quit();        
    }

    public void OnMenuButton()
    {
        SceneManager.LoadScene(0);
    }

    public void OnTutorialButton()
    {
        SceneManager.LoadScene(2);
    }

    public void OnSettingsButton()
    {
        SceneManager.LoadScene(3);
    }
}