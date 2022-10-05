using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Chrono : MonoBehaviour
{
    [SerializeField] int startTime;
    private float chronoTime;

    public TextMeshProUGUI chronoTxt;

    void Start()
    {
        chronoTime = startTime;
        DisplayTime(chronoTime, chronoTxt);
        StartCoroutine(countSeconds());
    }

    IEnumerator countSeconds()
    {
        yield return new WaitForSeconds(1f);
        
        chronoTime--;
        DisplayTime(chronoTime, chronoTxt);


        if (chronoTime <= 0)
        {
            StopCoroutine(countSeconds());
        }
        else
        {
            StartCoroutine(countSeconds());
        }       
    }

    void DisplayTime(float timeToDisplay, TextMeshProUGUI display)
    {
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        display.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
