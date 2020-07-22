using System;
using Crysberry.Routines;
using UnityEngine;

public class SetMoveDirectionOnTriggerBehaviour : MonoBehaviour
{
    private MovementBehaviour playerMovement;
    
    public MovementDirection Direction;
    public MovementBehaviour MovementBehaviour;
    public CharacterAnimationBehaviour Animator;

    private void Start()
    {
        playerMovement = GameObject.FindWithTag("Player").GetComponentInChildren<MovementBehaviour>();
    }

    private void Update()
    {
        if (Direction == MovementDirection.Right || Direction == MovementDirection.Left)
        {
            if (Mathf.Abs(playerMovement.transform.position.z - transform.position.z) < 0.3f &&
                (Direction == playerMovement.CurrentDirection || (playerMovement.CurrentDirection == MovementDirection.None && playerMovement.PendingDirection == Direction)))
            {
                if (Mathf.Abs(playerMovement.transform.position.x - transform.position.x) < 1f)
                {
                    Move();
                }
                else
                {
                    Stop();
                }
            }
        }
        else
        {
            if (Mathf.Abs(playerMovement.transform.position.x - transform.position.x) < 0.3f &&
                (Direction == playerMovement.CurrentDirection || (playerMovement.CurrentDirection == MovementDirection.None && playerMovement.PendingDirection == Direction)))
            {
                if (Mathf.Abs(playerMovement.transform.position.z - transform.position.z) < 1f)
                {
                    Move();
                }
                else
                {
                    Stop();
                }
            }
        }
    }

    private void Move()
    {
        if (MovementBehaviour.IsAbleToMoveInDirection(Direction))
        {
            AreaHelper.SetWalkable(MovementBehaviour.transform.position, true);
            MovementBehaviour.SetDirection(Direction);
            Animator.TriggerRun(true);
            
            if (playerMovement.CurrentDirection == MovementDirection.None && playerMovement.PendingDirection == Direction)
            {
                playerMovement.SetDirection(Direction);
            }
        }
        else
        {
            Stop();
            AreaHelper.SetWalkable(MovementBehaviour.transform.position, false);
        }
    }

    private void Stop()
    {
        Animator.TriggerRun(false);
        MovementBehaviour.SetDirection(MovementDirection.None);
    }
}