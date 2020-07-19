using System;
using Crysberry.Routines;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DestroyTimerBehaviour : MonoBehaviour
{
    private bool isTriggered;
    private float triggeredTimer;

    private bool invalidated;
    public float DestroyTime = 2f;
    public float TimeBeforeGameOver = 1f;
    public Image FillBar;
    public UnityEvent OnEndDanger;
    public UnityEvent OnDanger;
    public UnityEvent OnDestroyed;
    
    private void Start()
    {
#if UNITY_EDITOR
        if (!FindObjectOfType<AreaInitializeBehaviour>().DebugMode)
        {
            MovementBehaviour.WillEnterObstacleCell += OnWillEnterObstacle;
            MovementBehaviour.WillLeaveCell += MovementBehaviourOnWillLeaveCell;
            GameOutcomeBehaviour.OnLoose += GameOutcomeBehaviourOnOnLoose;
        }
#endif
    }

    private void GameOutcomeBehaviourOnOnLoose()
    {
        GameplayTimescale.GameActive = false;
        OnDestroyed.Invoke();
    }

    private void Update()
    {
        if (isTriggered && GameplayTimescale.GameActive)
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
                    if (!invalidated)
                    {
                        FindObjectOfType<GameOutcomeBehaviour>().ForceLoose();
                    }
                   
                }, TimeBeforeGameOver);
            }
        }
    }

    private void OnDisable()
    {
        invalidated = true;
        GameOutcomeBehaviour.OnLoose -= GameOutcomeBehaviourOnOnLoose;
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

    public void ForceDestroy()
    {
        GameplayTimescale.GameActive = false;
        OnDestroyed.Invoke();
        Routiner.InvokeDelayed(() =>
        {
            FindObjectOfType<GameOutcomeBehaviour>().ForceLoose();
        }, TimeBeforeGameOver);
    }
}