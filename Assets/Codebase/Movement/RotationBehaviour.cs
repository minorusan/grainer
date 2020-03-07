using DG.Tweening;
using UnityEngine;

public class RotationBehaviour : MonoBehaviour
{
    public MovementBehaviour Movement;

    private void Awake()
    {
        Movement.OwnerDirectionChanged += OnDirectionChanged;
    }

    private void OnDirectionChanged(GameObject sender, DirectionChangedEventArgs changedEventArgs)
    {
        if (changedEventArgs.Current != MovementDirection.None)
        {
            transform.DOLookAt(transform.position + changedEventArgs.Current.ToVector3(), 0.3f);
        }
    }
}