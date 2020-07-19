﻿using System;
using TMPro;
using UnityEngine;

public class SturtupTimerBehaviour : MonoBehaviour
{
    public float StartupTime = 3f;
    public TextMeshProUGUI TimeLeft;
    public static event Action TimerDisabled;
    
    void Update()
    {
        if (StartupTime > 0)
        {
            StartupTime -= Time.deltaTime;
            TimeLeft.text = ((int)StartupTime).ToString();
            GameplayTimescale.GameActive = false;
        }
        else
        {
            GameplayTimescale.GameActive = true;
            if (TutorialBehaviour.TutorialCompleted)
            {
                var player = GameObject.Find("Player");
                player.GetComponentInChildren<MovementBehaviour>().SetDirection(player.transform.forward.ToDirection());
            }
            TimerDisabled?.Invoke();
            enabled = false;
        }
    }
}