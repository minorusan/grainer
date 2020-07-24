using System;
using Codebase.Input;
using GameAnalyticsSDK;
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
        GameAnalytics.NewProgressionEvent (GAProgressionStatus.Start, $"level_{AppState.GameplayLevelNumber}", LevelsStorage.TurnsCountForLevelNumber(AppState.GameplayLevelNumber));
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
                LevelsStorage.UpdateLevel(AppState.GameplayLevelNumber, TurnsCounter.CurrentTurnsCount, FindObjectOfType<InputProviderBase>().GetLastHistory);
            }
            AppState.PassLevelIfNeeded();
            GameAnalytics.NewProgressionEvent (GAProgressionStatus.Complete, $"level_{AppState.GameplayLevelNumber}", TurnsCounter.CurrentTurnsCount);
            OnWin.Invoke();
        }
        else
        {
            GameAnalytics.NewProgressionEvent (GAProgressionStatus.Fail, $"level_{AppState.GameplayLevelNumber}", TurnsCounter.CurrentTurnsCount);
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