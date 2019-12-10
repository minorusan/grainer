using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Profiling;

public class MovementBehaviour : DebuggableBehaviour
{
    private Vector3 previousPosition;
    private Vector3 nextPosition;
    private MovementDirection pendingDirection = MovementDirection.None;
    private MovementDirection currentDirection = MovementDirection.None;

    public static event DirectionChangedHandler DirectionChanged = delegate(GameObject sender, DirectionChangedEventArgs changedEventArgs) { };
    public event DirectionChangedHandler OwnerDirectionChanged = delegate (GameObject sender, DirectionChangedEventArgs changedEventArgs) { };
    public event DirectionChangedHandler OwnerWillChangeDirection = delegate (GameObject sender, DirectionChangedEventArgs changedEventArgs) { };
    public static event Action<GameObject> MovementBegan = delegate (GameObject obj) { };
    public static event Action<GameObject> MovementEnded = delegate (GameObject obj) { };
    public static event CellCalbackHandler WillLeaveCell = delegate (GameObject obj, Vector3 pos) { };
    public static event CellCalbackHandler LeftCell = delegate (GameObject obj, Vector3 pos) { };
    public static event CellCalbackHandler WillEnterCell = delegate (GameObject obj, Vector3 pos) { };
    public static event CellCalbackHandler WillEnterObstacleCell = delegate (GameObject obj, Vector3 pos) { };
    public static event CellCalbackHandler EnteredCell = delegate (GameObject obj, Vector3 pos) { };

    public float PercentageTillNextPosition =>
        Extentions.InverseLerp(previousPosition, nextPosition, transform.position);

    public bool InvokesEvents;

    public UnityEvent OnRotate;
    

    public MovementSettings MovementSettings;

    private void Start()
    {
        nextPosition = transform.position;
    }

    public void SetDirection(MovementDirection direction)
    {
        pendingDirection = direction;
        if (currentDirection != direction)
        {
            OwnerWillChangeDirection(gameObject, new DirectionChangedEventArgs(currentDirection, direction));
        }
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
        if (!GameplayTimescale.GameActive)
        {
            return;
        }
        
        if (currentDirection != MovementDirection.None)
        {
            var currentPosition = transform.position;
            var distance = Vector3.Distance(currentPosition, nextPosition);
            if (distance < Constants.MOVEMENT_STOP_TRESHOLD)
            {
                if (InvokesEvents)
                {
                    LeftCell(gameObject, previousPosition);
                }
                
                if (InvokesEvents && previousPosition != nextPosition)
                {
                    EnteredCell(gameObject, currentPosition);
                }

                if (InvokesEvents && currentDirection != pendingDirection)
                {
                    OnRotate.Invoke();
                    DirectionChanged(gameObject, new DirectionChangedEventArgs(currentDirection, pendingDirection));   
                }
                currentDirection = pendingDirection;
                previousPosition = currentPosition;
                nextPosition = currentPosition + currentDirection.ToVector3();
                
                OwnerDirectionChanged(gameObject, new DirectionChangedEventArgs(currentDirection, pendingDirection));
                if (!nextPosition.IsWalkable())
                {
                    currentDirection = MovementDirection.None;
                    if (InvokesEvents)
                    {
                        WillEnterObstacleCell(gameObject, currentPosition);
                    }

                    nextPosition = currentPosition;
                    MovementEnded(gameObject);
                }
                else
                {
                    if (InvokesEvents)
                    {
                        WillLeaveCell(gameObject, currentPosition);
                        WillEnterCell(gameObject, nextPosition);
                    }
                }
            }
            else
            {
                var tile = AreaHelper.GetDefinition(currentPosition);
                if (tile != null)
                {
                    transform.position = Vector3.MoveTowards(currentPosition, nextPosition, MovementSettings.Speed * tile.SpeedCoefitient);
                }
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