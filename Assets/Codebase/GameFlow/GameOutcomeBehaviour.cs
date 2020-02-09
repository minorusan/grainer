using System;
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
        IsChampion = false;
    }

    public void Check()
    {
        if (GameplayObjectivesBehaviour.IsCompleted)
        {
            IsChampion = LevelsHistory.PassLevel(LevelsHistory.GamePlayLevelID, TurnsCounter.CurrentTurnsCount);
            OnWin.Invoke();
        }
        else
        {
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