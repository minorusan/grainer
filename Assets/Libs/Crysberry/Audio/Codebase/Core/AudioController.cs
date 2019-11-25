using Crysberry.Audio;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    private static AudioController instance;
    private AudioService audioService;

    private void Awake()
    {
        audioService = new AudioService();
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnDisable()
    {
        instance = null;
    }
    
    public static bool PlayAudio(AudioEffectDefinition audioEffect)
    {
        if (instance != null)
        {
            instance.audioService.Play(audioEffect, AudioPriority.High);
            return true;
        }
        else
        {
            Debug.Log("AudioController::Creating instance");
            new GameObject("AudioController").AddComponent<AudioController>();
            PlayAudio(audioEffect);
        }

        return false;
    }
}