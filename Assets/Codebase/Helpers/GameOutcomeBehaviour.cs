using UnityEngine;
using UnityEngine.Events;

public class GameOutcomeBehaviour : MonoBehaviour
{
    public UnityEvent OnWin;
    public UnityEvent OnLose;
    
    public void Check()
    {
        if (GameplayObjectivesBehaviour.IsCompleted)
        {
            LevelsHistory.PassLevel(LevelsHistory.GamePlayLevelID);
            OnWin.Invoke();
        }
        else
        {
            OnLose.Invoke();
        }
    }
}