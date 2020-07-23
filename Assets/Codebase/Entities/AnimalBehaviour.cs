using System;
using UnityEngine;

public class AnimalBehaviour : MonoBehaviour
{
    private MovementDirection currentDirection;
    public enum AnimalMoveDirection
    {
        Horizontal, Vertical
    }

    public AnimalMoveDirection Direction;
    public MovementBehaviour Movement;


    private void Update()
    {
        if (Direction == AnimalMoveDirection.Horizontal)
        {
            if (currentDirection == MovementDirection.None)
            {
                currentDirection = MovementDirection.Left;
            }
            
            if (Movement.IsAbleToMoveInDirection(currentDirection))
            {
                Movement.SetDirection(currentDirection);
            }
            else
            {
                currentDirection = currentDirection == MovementDirection.Left
                    ? MovementDirection.Right
                    : MovementDirection.Left;
            }
        }
        else
        {
            if (currentDirection == MovementDirection.None)
            {
                currentDirection = MovementDirection.Down;
            }
            
            if (Movement.IsAbleToMoveInDirection(currentDirection))
            {
                Movement.SetDirection(currentDirection);
            }
            else
            {
                currentDirection = currentDirection == MovementDirection.Down
                    ? MovementDirection.Up
                    : MovementDirection.Down;
            }
        }
    }
}