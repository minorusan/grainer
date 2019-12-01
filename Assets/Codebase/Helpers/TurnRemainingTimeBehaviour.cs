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
        Movement.OwnerWillChangeDirection += (sender, args) =>
        {
            var percantage = Movement.PercentageTillNextPosition;
            if (percantage < 0.7f)
            {
                TimerGroup.gameObject.SetActive(true);
                isWorking = true;
            }
        };
    }
    private void Update()
    {
        if (isWorking)
        {
            var percantage = Movement.PercentageTillNextPosition;
            if (percantage <= 0.8f)
            {
                TimerMask.fillAmount = 1f - percantage;
            }
            else
            {
                isWorking = false;
                TimerGroup.SetActive(false);
            }
        }
    }
}
