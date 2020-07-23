using System;
using UnityEngine;

public class AnimalBehaviour : MonoBehaviour
{
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
            if (Movement.IsAbleToMoveInDirection(MovementDirection.Left))
            {
                Movement.SetDirection(MovementDirection.Left);
            }
            else
            {
                Movement.SetDirection(MovementDirection.Right);
            }
        }
    }
}