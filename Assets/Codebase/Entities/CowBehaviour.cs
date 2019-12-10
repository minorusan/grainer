using System;
using Crysberry.Audio;
using Crysberry.Routines;
using UnityEngine;
using Random = UnityEngine.Random;

public class CowBehaviour : MonoBehaviour
{
    private bool invalidated;
    private float initialSpeed;
    private MovementDirection previousDirection;
    private MovementDirection[] directions = new[]
        {MovementDirection.Down, MovementDirection.Left, MovementDirection.Right, MovementDirection.Left};
    public MovementBehaviour MovementBehaviour;
    public float MaxDelay = 2;
    public GameObject AlertSign;
    public AudioEffectDefinition AudioEffectDefinition;
    [Range(0, 1)]
    public float WalkChance;

    public void Start()
    {
        HornButtonBehaviour.HornPlayed += HornButtonBehaviourOnHornPlayed;
        initialSpeed = MovementBehaviour.MovementSettings.Speed;
        MoveIfPossible();
    }

    private void HornButtonBehaviourOnHornPlayed(Vector3 obj)
    {
        if (AlertSign != null && Vector3.Distance(obj, transform.position) <= 3f)
        {
            MaxDelay = 0f;
            MovementBehaviour.MovementSettings.Speed *= 2f;
            AlertSign.gameObject.SetActive(true);
            Routiner.InvokeDelayed(() => { AudioController.PlayAudio(AudioEffectDefinition); }, Random.Range(0f, 0.5f));
            MoveIfPossible();
        }
    }

    private void OnDisable()
    {
        MovementBehaviour.MovementSettings.Speed = initialSpeed;
        HornButtonBehaviour.HornPlayed -= HornButtonBehaviourOnHornPlayed;
        invalidated = true;
    }

    private void MoveIfPossible()
    {
        var movementDirection = Random.value > WalkChance ? MovementDirection.None : directions[Random.Range(0, directions.Length - 1)];
        if (movementDirection == previousDirection)
        {
            MoveIfPossible();
            return;
        }

        previousDirection = movementDirection;
        var delay = Random.Range(0, MaxDelay);
        Routiner.InvokeDelayed(() =>
        {
            if (!invalidated)
            {
                MovementBehaviour.SetDirection(movementDirection);
                MoveIfPossible();
            }
        }, delay);
    }
}