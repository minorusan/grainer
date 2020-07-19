using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnableButtonBehaviour : MonoBehaviour
{
    private void OnEnable()
    {
        SturtupTimerBehaviour.TimerDisabled += OnTimerDisabled;
    }  
    private void OnDisable()
    {
        SturtupTimerBehaviour.TimerDisabled -= OnTimerDisabled;
    }

    private void OnTimerDisabled()
    {
        GetComponent<Button>().interactable = true;
    }
}
