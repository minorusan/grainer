﻿using Crysberry.Routines;
using UnityEngine;
using Random = UnityEngine.Random;

public class CritterBehaviour : MonoBehaviour
{
    private bool invalidated;
    private MovementDirection[] directions = new[]
        {MovementDirection.Down, MovementDirection.Left, MovementDirection.Right, MovementDirection.Left};
    public MovementBehaviour MovementBehaviour;
    public float MaxDelay = 2;
    [Range(0, 1)]
    public float WalkChance;

    public void Start()
    {
        Move();
    }

    private void OnDisable()
    {
        invalidated = true;
    }

    private void Move()
    {
        var movementDirection = Random.value > WalkChance ? MovementDirection.None : directions[Random.Range(0, directions.Length - 1)];
        var delay = Random.Range(0, MaxDelay);
        Routiner.InvokeDelayed(() =>
        {
            if (invalidated)
            {
                return;
            }
            MovementBehaviour.SetDirection(movementDirection);
            Move();
        }, delay);
    }
}