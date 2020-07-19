using System;
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
        GameAnalytics.NewProgressionEvent (GAProgressionStatus.Start, $"level_{LevelsHistory.GamePlayLevelID}", LevelsHistory.TurnsCountForLevel(LevelsHistory.GamePlayLevelID));
        IsChampion = false;
    }

    public void Check()
    {
        if (GameplayObjectivesBehaviour.IsCompleted)
        {
            IsChampion = LevelsHistory.PassLevel(LevelsHistory.GamePlayLevelID, TurnsCounter.CurrentTurnsCount);
            GameAnalytics.NewProgressionEvent (GAProgressionStatus.Complete, $"level_{LevelsHistory.GamePlayLevelID}", TurnsCounter.CurrentTurnsCount);
            OnWin.Invoke();
        }
        else
        {
            GameAnalytics.NewProgressionEvent (GAProgressionStatus.Fail, $"level_{LevelsHistory.GamePlayLevelID}", TurnsCounter.CurrentTurnsCount);
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