using Crysberry.Audio;
using Crysberry.Routines;
using UnityEngine;

[CreateAssetMenu(fileName = "New audio event", menuName = "Grainer/Events/Audio")]
public class PlayAudioCellEvent : EventDefinition
{
    public AudioEffectDefinition AudioEffect;
    public int PlayCount = 1;
    public float PlayDelay;
    
    protected override void InvokeEvent(GameObject cell)
    {
        for (int i = 0; i < PlayCount; i++)
        {
            Routiner.InvokeDelayed(() =>
            {
                AudioController.PlayAudio(AudioEffect);
            }, i * PlayDelay);
        }
    }
}