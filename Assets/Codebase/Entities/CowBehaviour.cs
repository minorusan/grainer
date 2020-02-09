using System;
using System.Collections.Generic;
using Crysberry.Audio;
using Crysberry.Routines;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class CowBehaviour : MonoBehaviour
{
    private bool invalidated;
    private List<Vector3> positions = new List<Vector3>();
    private int currentPositionIndex = 7;
    private bool affectedByHorn;
    private Transform player;
    private Tween lookAt;

    private MovementDirection previousDirection;
    private MovementDirection[] directions = new[]
        {MovementDirection.Down, MovementDirection.Left, MovementDirection.Right, MovementDirection.Left};
    public MovementBehaviour MovementBehaviour;
    public float MaxDelay = 2;
    public GameObject AlertSign;
    public DOTweenAnimation Jumping;
    public AudioEffectDefinition AudioEffectDefinition;
  

    public void Start()
    {
        HornButtonBehaviour.HornPlayed += HornButtonBehaviourOnHornPlayed;
        player = GameObject.FindWithTag("Player").transform;
        var startPosition = transform.position;
        positions.Add(startPosition + new Vector3(-1, 0f, -1));
        positions.Add(startPosition + new Vector3(-1, 0f, 0));
        positions.Add(startPosition + new Vector3(-1, 0f, 1));
        positions.Add(startPosition + new Vector3(0, 0f, 1));
        positions.Add(startPosition + new Vector3(1, 0f, 1));
        positions.Add(startPosition + new Vector3(1, 0f, 0));
        positions.Add(startPosition + new Vector3(1, 0f, -1));
        positions.Add(startPosition + new Vector3(0, 0f, -1));
    }

    private void HornButtonBehaviourOnHornPlayed(Vector3 obj)
    {
        if (AlertSign != null && Vector3.Distance(obj, transform.position) <= 5f)
        {
            MaxDelay = 0f;
            AlertSign.gameObject.SetActive(true);
            Routiner.InvokeDelayed(() => { AudioController.PlayAudio(AudioEffectDefinition); }, Random.Range(0f, 0.5f));
            Routiner.InvokeDelayed(() =>
            {
                affectedByHorn = false; 
                AlertSign.gameObject.SetActive(false);
                MovementBehaviour.enabled = true;
                MoveIfPossible();
            }, 5f);
            affectedByHorn = true;
            lookAt = transform.DOLookAt(player.position, 0.1f);
            MovementBehaviour.enabled = false;
        }
    }

    private void FixedUpdate()
    {
        if (!affectedByHorn)
        {
            MoveIfPossible();
        }
        else
        {
            if (lookAt == null || !lookAt.active)
            {
                transform.LookAt(player);
            }
        }
    }

    private void OnDisable()
    {
        HornButtonBehaviour.HornPlayed -= HornButtonBehaviourOnHornPlayed;
        invalidated = true;
    }

    private void MoveIfPossible()
    {
        var target = positions[currentPositionIndex];
        if (!AreaHelper.IsWalkable(target))
        {
            affectedByHorn = true;
            MovementBehaviour.enabled = false;
            if (Jumping != null)
            {
                lookAt = transform.DOLookAt(player.position, 0.1f);
                Destroy(Jumping);
            }
           
            return;
        }
        if (Vector3.Distance(transform.position, target) > 0.1f)
        {
            var direction = MovementDirection.None;
            if (Mathf.Abs(target.x - transform.position.x) < 0.1f)
            {
                direction = target.z > transform.position.z ? MovementDirection.Up : MovementDirection.Down;
            }
            else
            {
                direction = target.x > transform.position.x ? MovementDirection.Right : MovementDirection.Left;
            }
            MovementBehaviour.SetDirection(direction);
        }
        else
        {
            currentPositionIndex++;
            if (currentPositionIndex >= positions.Count)
            {
                currentPositionIndex = 0;
            }
        }
    }
}