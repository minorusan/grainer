using System;
using UnityEngine;

public class MovementBehaviour : DebuggableBehaviour
{
    private MovementDirection pendingDirection = MovementDirection.None;
    private MovementDirection currentDirection = MovementDirection.None;

    public static event DirectionChangedHandler DirectionChanged = delegate(GameObject sender, DirectionChangedEventArgs changedEventArgs) { };
    public static event Action<GameObject> MovementBegan = delegate (GameObject obj) { };
    public static event Action<GameObject> MovementEnded = delegate (GameObject obj) { };

    private Vector3 nextPosition;

    public MovementSettings MovementSettings;

    private void Start()
    {
        nextPosition = transform.position;
    }

    public void SetDirection(MovementDirection direction)
    {
        pendingDirection = direction;
        if (currentDirection == MovementDirection.None)
        {
            currentDirection = pendingDirection;
            MovementBegan(gameObject);
        }
    }

    private void FixedUpdate()
    {
        MoveIfNeeded();
    }

    private void MoveIfNeeded()
    {
        if (currentDirection != MovementDirection.None)
        {
            var currentPosition = transform.position;
            var distance = Vector3.Distance(currentPosition, nextPosition);
            if (distance < Constants.MOVEMENT_STOP_TRESHOLD)
            {
                if (currentDirection != pendingDirection)
                {
                    DirectionChanged(gameObject, new DirectionChangedEventArgs(currentDirection, pendingDirection));
                }
                currentDirection = pendingDirection;
                nextPosition = currentPosition + currentDirection.ToVector3();
                if (!nextPosition.IsWalkable())
                {
                    currentDirection = MovementDirection.None;
                    nextPosition = currentPosition;
                    MovementEnded(gameObject);
                }
            }
            else
            {
                transform.position = Vector3.MoveTowards(currentPosition, nextPosition, MovementSettings.Speed);
            }
        }
    }

    protected override void GizmosDebug()
    {
        if (currentDirection != MovementDirection.None)
        {
            Gizmos.color = nextPosition.IsWalkable() ? Color.cyan : Color.red;
            Gizmos.DrawLine(transform.position, nextPosition);
        }
    }
}