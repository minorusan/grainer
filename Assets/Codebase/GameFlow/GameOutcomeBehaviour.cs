using UnityEngine;
using UnityEngine.Events;

public class GameOutcomeBehaviour : MonoBehaviour
{
    public GameplayTurnsCountBehaviour TurnsCounter;
    public UnityEvent OnWin;
    public UnityEvent OnLose;

    public void Check()
    {
        if (GameplayObjectivesBehaviour.IsCompleted)
        {
            LevelsHistory.PassLevel(LevelsHistory.GamePlayLevelID, TurnsCounter.CurrentTurnsCount);
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
}