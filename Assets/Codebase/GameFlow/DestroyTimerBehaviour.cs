using System;
using Crysberry.Routines;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DestroyTimerBehaviour : MonoBehaviour
{
    private bool isTriggered;
    private float triggeredTimer;
    
    public float DestroyTime = 2f;
    public float TimeBeforeGameOver = 1f;
    public Image FillBar;
    public UnityEvent OnEndDanger;
    public UnityEvent OnDanger;
    public UnityEvent OnDestroyed;
    
    private void Start()
    {
        MovementBehaviour.WillEnterObstacleCell += OnWillEnterObstacle;
        MovementBehaviour.WillLeaveCell += MovementBehaviourOnWillLeaveCell;
    }

    private void Update()
    {
        if (isTriggered)
        {
            triggeredTimer += Time.deltaTime;
            FillBar.fillAmount = triggeredTimer / DestroyTime;
            if (FillBar.fillAmount >= 1f)
            {
                isTriggered = false;
                GameplayTimescale.GameActive = false;
                OnDestroyed.Invoke();
                Routiner.InvokeDelayed(() =>
                {
                    FindObjectOfType<GameOutcomeBehaviour>().ForceLoose();
                }, TimeBeforeGameOver);
            }
        }
    }

    private void OnDisable()
    {
        MovementBehaviour.WillEnterObstacleCell -= OnWillEnterObstacle;
        MovementBehaviour.WillLeaveCell -= MovementBehaviourOnWillLeaveCell;
    }

    private void MovementBehaviourOnWillLeaveCell(GameObject sender, Vector3 cellposition)
    {
        if (isTriggered)
        {
            Debug.Log("UnTriggered");
            OnEndDanger.Invoke();
            isTriggered = false;
        }
    }

    private void OnWillEnterObstacle(GameObject sender, Vector3 cellposition)
    {
        isTriggered = true;
        OnDanger.Invoke();
        Debug.Log("Triggered");
    }
}