using Crysberry.Routines;
using UnityEngine;

public class SetMoveDirectionOnTriggerBehaviour : MonoBehaviour
{
    private MovementBehaviour playerMovement;
    public MovementDirection Direction;
    public MovementBehaviour MovementBehaviour;
    public CharacterAnimationBehaviour Animator;

    private void OnTriggerEnter(Collider other)
    {
        if (playerMovement == null)
        {
            playerMovement = GameObject.FindWithTag("Player").GetComponentInChildren<MovementBehaviour>();
        }

        if (playerMovement.CurrentDirection == Direction)
        {
            if (MovementBehaviour.IsAbleToMoveInDirection(Direction))
            {
                MovementBehaviour.SetDirection(Direction);
                AreaHelper.SetWalkable(MovementBehaviour.transform.position, true);
                Animator.TriggerRun(true);
            }
            else
            {
                Animator.TriggerRun(false);
                MovementBehaviour.SetDirection(MovementDirection.None);
                AreaHelper.SetWalkable(MovementBehaviour.transform.position, false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
       MovementBehaviour.SetDirection(MovementDirection.None);
       Animator.TriggerRun(false);
    }
}