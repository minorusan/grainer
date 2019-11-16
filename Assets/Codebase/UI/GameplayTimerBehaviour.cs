using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameplayTimerBehaviour : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    private int seconds;
    private bool isWork;

   private void Start()
    {
        isWork = true;
        seconds = 0;
        StartCoroutine(WorkerTimer());
    }

    private IEnumerator WorkerTimer()
    {
        var waitForOneSeconds = new WaitForSeconds(1);
        TimeSpan timeSpan;
        while (isWork)
        {
            yield return waitForOneSeconds;

            seconds++;

            timeSpan = TimeSpan.FromSeconds(seconds);
            timerText.text = $"{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";
        }
    }

    public int GetTime()
    {
        return seconds;
    }
}