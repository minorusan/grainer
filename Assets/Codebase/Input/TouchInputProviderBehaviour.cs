using System;
using UnityEngine;

public class TouchInputProviderBehaviour : InputProviderBase
{
    private RotationBehaviour player;
    
    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponentInChildren<RotationBehaviour>();
    }

    void Update()
    {
        var direction = GetDirection();
        if (direction != MovementDirection.None)
        {
            Debug.LogWarning($"Returning::::::: {direction}");
            InvokeEvent(direction);
        }
    }
    
    private MovementDirection GetDirection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var direction = Input.mousePosition.x > Screen.width * 0.5f ? MovementDirection.Right : MovementDirection.Left;
            var playerDirection = player.transform.forward.ToDirection();
            if (direction == MovementDirection.Left)
            {
                switch (playerDirection)
                {
                    case MovementDirection.Left:
                        return MovementDirection.Down;
                    case MovementDirection.Right:
                        return MovementDirection.Up;
                    case MovementDirection.Up:
                        return MovementDirection.Left;
                    case MovementDirection.Down:
                        return MovementDirection.Right;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            else
            {
                switch (playerDirection)
                {
                    case MovementDirection.Left:
                        return MovementDirection.Up;
                    case MovementDirection.Right:
                        return MovementDirection.Down;
                    case MovementDirection.Up:
                        return MovementDirection.Right;
                    case MovementDirection.Down:
                        return MovementDirection.Left;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
        return MovementDirection.None;
    }
}