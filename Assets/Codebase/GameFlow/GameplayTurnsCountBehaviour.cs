using System;
using Crysberry.Routines;
using UnityEngine;

public class GameplayTurnsCountBehaviour : MonoBehaviour
{
    private MovementDirection previous = MovementDirection.None;

    public static event Action<int> PlayerTurnsCountChanged = delegate(int i) {  }; 
    public int CurrentTurnsLeftCount { get; private set; }
    public int CurrentTurnsCount { get; private set; }

    private void OnEnable()
    {
        var min = LevelsHistory.TurnsCountForLevel(LevelsHistory.GamePlayLevelID + 1);

        CurrentTurnsLeftCount = min;
        PlayerTurnsCountChanged(CurrentTurnsLeftCount);
        var area = FindObjectOfType<AreaInitializeBehaviour>();
        Routiner.InvokeNextFrame(() =>
        {
            GameObject.FindWithTag("Player").GetComponent<MovementBehaviour>().OwnerDirectionChanged +=
                (sender, args) =>
                {
                    if (previous != args.Current && !area.DebugMode)
                    {
                        CurrentTurnsLeftCount--;
                        CurrentTurnsCount++;
                        PlayerTurnsCountChanged(CurrentTurnsLeftCount);
                        Debug.Log($"GameplayTurnsCountBehaviour::Current turns count is {CurrentTurnsLeftCount}. Previous {previous}, current {args.Current}");
                        previous = args.Current;
                        if (CurrentTurnsLeftCount < 0)
                        {
                            FindObjectOfType<DestroyTimerBehaviour>().ForceDestroy();
                        }
                    }
                };
        });
    }
}
