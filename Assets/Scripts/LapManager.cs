using System;
using TMPro;
using UnityEngine;

public class LapManager : MonoBehaviour
{
    [SerializeField] private BoxCollider2D line;
    [SerializeField] private BoxCollider2D checkPoint;
    [SerializeField] private Collider2D car;
    [SerializeField] TMP_Text text;
    private bool checkPointPassed = false;
    private int lapCounter = 1;

    // Update is called once per frame
    void Update()
    {
        if(line.IsTouching(car) && checkPointPassed)
        {
            lapCounter++;
            checkPointPassed = false;
            Debug.Log(lapCounter);
        }
        if(checkPoint.IsTouching(car) && !checkPointPassed)
        {
            checkPointPassed = true;
        }
        text.text = "Tour : " + lapCounter;
    }
}
