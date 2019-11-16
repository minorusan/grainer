using Crysberry.Routines;
using UnityEngine;

public class GameplayTurnsCountBehaviour : MonoBehaviour
{
    private MovementDirection previous = MovementDirection.None;
    public int CurrentTurnsCount { get; private set; }

    private void OnEnable()
    {
        Routiner.InvokeNextFrame(() =>
        {
            GameObject.FindWithTag("Player").GetComponent<MovementBehaviour>().OwnerDirectionChanged +=
                (sender, args) =>
                {
                    if (previous != args.Current)
                    {
                        CurrentTurnsCount++;
                        Debug.Log($"GameplayTurnsCountBehaviour::Current turns count is {CurrentTurnsCount}. Previous {previous}, current {args.Current}");
                        previous = args.Current;
                    }
                };
        });
    }
}
