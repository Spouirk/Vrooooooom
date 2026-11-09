using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LapManager : MonoBehaviour
{
    [SerializeField] private int totalLapCount;
    [SerializeField] private BoxCollider2D line;
    [SerializeField] private BoxCollider2D checkPoint;
    [SerializeField] private Collider2D car;
    [SerializeField] private TMP_Text text;
    [SerializeField] private GameObject inGameUi;
    [SerializeField] private GameObject victoryUi;
    [SerializeField] private TMP_Text finalTimeDisplay;
    [SerializeField] private Timer timer;
    [SerializeField] private GameObject firstButton;
    private bool checkPointPassed = false;
    private int lapCounter = 1;

    void Start()
    {
        victoryUi.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(line.IsTouching(car) && checkPointPassed)
        {
            lapCounter++;
            checkPointPassed = false;
            if (lapCounter > totalLapCount)
            {
                Time.timeScale = 0;
                timer.displayTime(finalTimeDisplay);
                inGameUi.SetActive(false);
                victoryUi.SetActive(true);
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(firstButton);
            }
        }
        if(checkPoint.IsTouching(car) && !checkPointPassed)
        {
            checkPointPassed = true;
        }
        text.text = "Lap : " + lapCounter + "/" + totalLapCount;
    }
}
