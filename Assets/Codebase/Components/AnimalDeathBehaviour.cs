using Crysberry.Audio;
using UnityEngine;

public class AnimalDeathBehaviour : MonoBehaviour
{
    public AudioEffectDefinition Meat;
    private void OnCollisionEnter(Collision other)
    {
        GameplayTimescale.GameActive = false;
        FindObjectOfType<GameOutcomeBehaviour>().ForceLoose();
        AudioController.PlayAudio(Meat);
    }
}
