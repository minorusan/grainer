using UnityEngine;
using UnityEngine.Events;

public class GameFinishedBroadcastBehaviour : MonoBehaviour
{
    private static GameFinishedBroadcastBehaviour instance;

    public UnityEvent LevelCompleted;

    private void OnEnable()
    {
        instance = this;
    }

    private void OnDisable()
    {
        instance = null;
    }

    public static void InvokeLevelFinished()
    {
        if (instance != null)
        {
            instance.LevelCompleted.Invoke();
        }
    }
}