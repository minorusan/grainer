using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StopWatchBehaviour : MonoBehaviour
{
    private bool isWorking;
    private float workTime;
    public MovementBehaviour Movement;
    public GameObject TimerGroup;
    public Image TimerMask;
    public float StopTime = 3f;

    public void BeginTimer()
    {
        TimerGroup.gameObject.SetActive(true);
        isWorking = true;
        workTime = 0f;
        Movement.SetDirection(MovementDirection.None);
    }
    
    public void EndTimer()
    {
        if (isWorking)
        {
            isWorking = false;
            TimerGroup.SetActive(false);
            Movement.SetDirection(Movement.PreviousDirection, true);
        }
    }


    private void Update()
    {
        if (isWorking)
        {
            workTime += Time.deltaTime;
            var percentage = workTime/StopTime;
            if (percentage <= 0.9f)
            {
                TimerMask.fillAmount = 1f - percentage;
            }
            else
            {
                EndTimer();
            }
        }
    }
}
