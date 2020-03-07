using Crysberry.Audio;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class CharacterMessagesBehaviour : MonoBehaviour
{
    private float timeSinceLastMessage;
    public LocalizableStringBase AlertString;
    public GameObject MessagePrefab;
    public AudioEffectDefinition Gasp;
    
    public void TriggerAlertMessage()
    {
        if (Random.value > 0.5f && Time.time - timeSinceLastMessage > 2f) 
        {
            var instance = Instantiate(MessagePrefab, transform).GetComponentInChildren<TextMeshProUGUI>();
            instance.text = AlertString.Value;
            AudioController.PlayAudio(Gasp);
            timeSinceLastMessage = Time.time;
        }
    }
}
