using System;
using Codebase.Input;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Profiling;

public class MovementBehaviour : DebuggableBehaviour
{
    private Vector3 previousPosition;
    private Vector3 nextPosition;
    private MovementDirection pendingDirection = MovementDirection.None;
    private MovementDirection currentDirection = MovementDirection.None;
    private MovementDirection previousDirection = MovementDirection.None;
    private InputProviderBase inputProvider;

    public bool IgnoresTimescale;
    public bool IgnoresObstacles;
    public bool IsPlayer;
    public MovementDirection CurrentDirection => currentDirection;
    public MovementDirection PreviousDirection => previousDirection;
    public MovementDirection PendingDirection => pendingDirection;

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

    public bool IsAbleToMoveInDirection(MovementDirection direction)
    {
        if (IgnoresObstacles)
        {
            return true;
        }
        var nextCellPosition = transform.position + direction.ToVector3();
        return AreaHelper.IsWalkable(nextCellPosition);
    }

    private void MoveIfNeeded()
    {
        if (!GameplayTimescale.GameActive && !IgnoresTimescale)
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
                
                previousDirection = currentDirection;
                currentDirection = pendingDirection;
                previousPosition = currentPosition;
                nextPosition = currentPosition + currentDirection.ToVector3();
                
                OwnerDirectionChanged(gameObject, new DirectionChangedEventArgs(currentDirection, pendingDirection));
                var canMove = IgnoresObstacles || nextPosition.IsWalkable();
                if (!canMove)
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
                    if (inputProvider == null)
                    {
                        inputProvider = FindObjectOfType<InputProviderBase>();
                    }

                    if (IsPlayer && currentDirection != previousDirection)
                    {
                        inputProvider.AddInput(currentDirection, currentPosition);
                    }
                    
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