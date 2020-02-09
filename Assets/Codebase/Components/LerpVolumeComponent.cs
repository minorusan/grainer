using DG.Tweening;
using UnityEngine;

public class LerpVolumeComponent : MonoBehaviour
{
    private AudioSource source;
    public float LerpTo = 1f;

    public float Duration = 0.5f;

    public void Lerp(bool inverse)
    {
        if (!AudioSettingsBehaviour.SoundEnabled)
        {
            return;
        }
        if (source == null)
        {
            source = GetComponent<AudioSource>();
        }
        source.DOComplete();
        source.DOFade(inverse ? 0f : LerpTo, Duration);
    }
}