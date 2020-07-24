using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerRotationBehaviour : MonoBehaviour
{
    public MovementBehaviour Movement;
    [SerializeField] private Collider rotorCollider;
    private void Awake()
    {
        if (Movement != null)
        {
            Movement.OwnerDirectionChanged += OnDirectionChanged;
            Movement.OwnerWillChangeDirection += OnDirectionWillChange;
        }
    }
    
    public void SetMover(MovementBehaviour mover)
    {
        Movement = mover;
        Awake();
    }

    private void OnDirectionWillChange(GameObject sender, DirectionChangedEventArgs changedeventargs)
    {
        if (rotorCollider == null)
        {
            return;
        }
        rotorCollider.enabled = false;
    }

    private void OnDirectionChanged(GameObject sender, DirectionChangedEventArgs changedEventArgs)
    {
        if (changedEventArgs.Current != MovementDirection.None)
        {
            var tweener = transform.DOLookAt(transform.position + changedEventArgs.Current.ToVector3(), 0.3f);
            tweener.onComplete += () => { rotorCollider.enabled = true; };
        }
    }
}
