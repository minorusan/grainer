using System;
using UnityEngine;
using UnityEngine.UI;

public class TurnRemainingTimeBehaviour : MonoBehaviour
{
    private bool isWorking;
    
    public MovementBehaviour Movement;
    public GameObject TimerGroup;
    public Image TimerMask;

    private void OnEnable()
    {
        Movement.OwnerWillChangeDirection += MovementOnOwnerWillChangeDirection;
    }

    private void MovementOnOwnerWillChangeDirection(GameObject sender, DirectionChangedEventArgs changedeventargs)
    {
        var percantage = Movement.PercentageTillNextPosition;
        if (percantage < 0.7f)
        {
            TimerGroup.gameObject.SetActive(true);
            isWorking = true;
        }
    }

    private void OnDisable()
    {
        Movement.OwnerWillChangeDirection -= MovementOnOwnerWillChangeDirection;
    }

    private void Update()
    {
        if (isWorking)
        {
            var percentage = Movement.PercentageTillNextPosition;
            if (percentage <= 0.8f)
            {
                TimerMask.fillAmount = 1f - percentage;
            }
            else
            {
                isWorking = false;
                TimerGroup.SetActive(false);
            }
        }
    }
}
