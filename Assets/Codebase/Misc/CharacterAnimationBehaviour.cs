using System;
using UnityEngine;

public class CharacterAnimationBehaviour : MonoBehaviour
{
    public Animator Animator;
    public CharacterMessagesBehaviour MessagesBehaviour;

    private void Awake()
    {
        if (Animator == null)
        {
            Animator = GetComponentInChildren<Animator>();
            MessagesBehaviour = GetComponentInChildren<CharacterMessagesBehaviour>();
        }
    }

    public void TriggerRun(bool trigger)
    {
        Animator.SetBool("run", trigger);
        MessagesBehaviour.TriggerAlertMessage();
    }
}