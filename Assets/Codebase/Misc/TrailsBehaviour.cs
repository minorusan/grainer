using System.Collections;
using System.Collections.Generic;
using Crysberry.Routines;
using DG.Tweening;
using UnityEngine;

public class TrailsBehaviour : MonoBehaviour
{
    private MovementDirection previous;
    public MovementBehaviour Movement;
    public TrailRenderer Trail;
    public float DetachDelay;
    public float FadeDuration;

    private void Awake()
    {
        Movement.OwnerDirectionChanged += OnDirectionChanged;
    }

    private void OnDirectionChanged(GameObject sender, DirectionChangedEventArgs changedEventArgs)
    {
        if (changedEventArgs.Current == previous)
        {
            return;
        }

        previous = changedEventArgs.Current;
        var instance = Instantiate(Trail, transform);
        instance.transform.localPosition = Vector3.zero;
        instance.material = new Material(instance.material);
        Routiner.InvokeDelayed(() =>
        {
            instance.transform.parent = null;
            instance.material.DOFade(0f, FadeDuration).OnComplete(() => { Destroy(instance.gameObject); });
        }, DetachDelay);
    }
}