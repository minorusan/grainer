using System;
using Crysberry.Audio;
using UnityEngine;
using UnityEngine.UI;

public class HornButtonBehaviour : MonoBehaviour
{
    public static event Action<Vector3> HornPlayed = delegate(Vector3 vector3) {  };
    private Button Button;
    public AudioEffectDefinition HornSound;
    
    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void PlayHorn()
    {
        AudioController.PlayAudio(HornSound);
        var player = GameObject.FindWithTag("Player");
        HornPlayed(player.transform.position);
        Button.interactable = false;
    }
}