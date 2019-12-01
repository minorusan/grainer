using System;
using System.Collections;
using System.Collections.Generic;
using Crysberry.Audio;
using Crysberry.Routines;
using UnityEngine;

public class EngineAudioBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public MovementBehaviour Movement;
    public AudioSource ActiveEngineSource;
    public AudioSource IdleEngineSource;
    public AudioEffectDefinition AccelerateSoundEffect;
    
    void Start()
    {
        MovementBehaviour.MovementBegan += MovementBehaviourOnMovementBegan;
        MovementBehaviour.MovementEnded += MovementBehaviourOnMovementEnded;
    }

    private void OnDisable()
    {
        MovementBehaviour.MovementBegan -= MovementBehaviourOnMovementBegan;
        MovementBehaviour.MovementEnded -= MovementBehaviourOnMovementEnded;
    }

    private void MovementBehaviourOnMovementEnded(GameObject obj)
    {
        if (obj.tag == Movement.tag)
        {
            Routiner.StartCouroutine(LerpAudio(ActiveEngineSource, ActiveEngineSource.volume, 0f, 0.5f));
        }
    }

    private void MovementBehaviourOnMovementBegan(GameObject obj)
    {
        if (obj.tag == Movement.tag)
        {
            AudioController.PlayAudio(AccelerateSoundEffect);    
            Routiner.StartCouroutine(LerpAudio(ActiveEngineSource, 0f, IdleEngineSource.volume * 1.3f, 1f));
        }
    }

    private IEnumerator LerpAudio(AudioSource audioSource, float from, float to, float duration)
    {
        var timePassed = 0f;
        var wait = new WaitForEndOfFrame();
        var percentage = 0f;
        while (percentage <= 1f || audioSource != null)
        {
            timePassed += Time.unscaledDeltaTime;
            percentage = timePassed / duration;
            audioSource.volume = Mathf.Lerp(from, to, percentage);
            
            yield return wait;
        }
    }
}
