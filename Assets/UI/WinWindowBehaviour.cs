using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinWindowBehaviour : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeTextMesh;
    [SerializeField] private TextMeshProUGUI turnsTextMesh;
    [SerializeField] private GameObject[] stars;
    private GameplayTimerBehaviour timer;
    private GameplayTurnsCounterBehaviour turnCounter;

    private void OnEnable()
    {
        if (timer == null)
        {
            timer = FindObjectOfType<GameplayTimerBehaviour>();
        }

        if (turnCounter == null)
        {
            turnCounter = FindObjectOfType<GameplayTurnsCounterBehaviour>();
        }
        
        FindObjectOfType<GameFinishedBroadcastBehaviour>().LevelCompleted.AddListener(() =>
        {
            var time = timer.GetTime();
            var timeSpan = TimeSpan.FromSeconds(time);
            timeTextMesh.text = $"{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";

            var turnCount = turnCounter.GetCurrentTurnsCount();
            turnsTextMesh.text = turnCount.ToString();
        });
    }
}