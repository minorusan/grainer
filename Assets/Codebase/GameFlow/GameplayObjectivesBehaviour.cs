using System;
using UnityEngine;

public class GameplayObjectivesBehaviour : MonoBehaviour
{
    private static GameplayObjectivesBehaviour instance;
    private int currentObjectiveState;

    public static bool IsCompleted => ((float)instance.currentObjectiveState / AreaHelper.ObjectivesCount) >= 1f;
    public static event Action<float> ObjectiveStateChanged = delegate(float f) {  };

    private void OnEnable()
    {
        instance = this;
    }

    private void OnDisable()
    {
        instance = null;
    }

    private void Start()
    {
        ObjectiveStateChanged(0f);
    }

    public static void Increament()
    {
        if (instance == null)
        {
            Debug.LogError("WTF IS GOING ON?!");
            return;
        }
        instance.currentObjectiveState++;
        var percentage = (float) instance.currentObjectiveState / AreaHelper.ObjectivesCount;
        Debug.Log($"GameplayObjectivesBehaviour:: Collected {instance.currentObjectiveState} of {AreaHelper.ObjectivesCount} objectives ({percentage * 100f}%)");
        ObjectiveStateChanged(percentage);
    }
}