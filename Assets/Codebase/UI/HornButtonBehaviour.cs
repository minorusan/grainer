using System;
using Crysberry.Audio;
using UnityEngine;
using UnityEngine.UI;
using WaterRippleForScreens;

public class HornButtonBehaviour : MonoBehaviour
{
    public static event Action<Vector3> HornPlayed = delegate(Vector3 vector3) {  };
    private Button Button;
    private RippleEffect RippleEffect;
    public AudioEffectDefinition HornSound;
    
    private void Start()
    {
        Button = GetComponent<Button>();
        RippleEffect = FindObjectOfType<RippleEffect>();
        Button.onClick.AddListener(PlayHorn);
    }

    public void PlayHorn()
    {
        AudioController.PlayAudio(HornSound);
        var player = GameObject.FindWithTag("Player");
        HornPlayed(player.transform.position);
        var playerScreenPosition = Camera.main.WorldToScreenPoint(player.transform.position);
        RippleEffect.SetNewRipplePosition(playerScreenPosition);
        Button.interactable = false;
    }
}