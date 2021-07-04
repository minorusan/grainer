using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameOutcomeBehaviour : MonoBehaviour
{
    public GameplayTurnsCountBehaviour TurnsCounter;
    public UnityEvent OnWin;
    public UnityEvent OnLose;
    public static event Action OnLoose = delegate {  };
    public static bool IsChampion;

    private void OnEnable()
    {
        var dictValue = new Dictionary<string, object>()
        {
            {"level_number", AppState.GameplayLevelNumber}
        };
        Amplitude.Instance.logEvent("level_start", dictValue);
        IsChampion = false;
    }

    public void Check()
    {
        if (GameplayObjectivesBehaviour.IsCompleted)
        {
            IsChampion =
                LevelsStorage.IsPlayerResultBetterThenRemote(AppState.GameplayLevelNumber,
                    TurnsCounter.CurrentTurnsCount);
            if (IsChampion)
            {
                LevelsStorage.UpdateLevel(AppState.GameplayLevelNumber, TurnsCounter.CurrentTurnsCount);
            }
            AppState.PassLevelIfNeeded();
            var dictValue = new Dictionary<string, object>()
            {
                {"level_number", AppState.GameplayLevelNumber},
                {"turns_count", TurnsCounter.CurrentTurnsCount}
            };
            Amplitude.Instance.logEvent("level_win", dictValue);
            OnWin.Invoke();
        }
        else
        {   
            var dictValue = new Dictionary<string, object>()
            {
                {"level_number", AppState.GameplayLevelNumber},
                {"turns_count", TurnsCounter.CurrentTurnsCount}
            };
            Amplitude.Instance.logEvent("level_fail", dictValue);
            OnLose.Invoke();
        }
    }

    public void ForceLoose()
    {
        OnLose.Invoke();
    }
    
    public void ForceWin()
    {
        OnWin.Invoke();
    }
}