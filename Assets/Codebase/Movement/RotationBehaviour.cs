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
        transform.LookAt(transform.position + changedEventArgs.Current.ToVector3());
    }
}