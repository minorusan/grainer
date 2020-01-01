using System;
using Crysberry.Routines;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class BoolEvent : UnityEvent<bool>
{
}

public class AudioSettingsBehaviour : MonoBehaviour
{
    private const string SFX_ENABLED_KEY = "sfjkdlsa;jfdkla;";
    private const string MUSIC_ENABLED_KEY = "43j9igsjdf89g0uj45g45";

    public BoolEvent SoundSettingChanged;
    public BoolEvent MusicSettingChanged;
    public static event Action<bool> SoundChanged;
    public static event Action<bool> MusicChanged;

    public static bool SoundEnabled => !PlayerPrefs.HasKey(SFX_ENABLED_KEY);
    public static bool MusicEnabled => !PlayerPrefs.HasKey(MUSIC_ENABLED_KEY);

    private void OnEnable()
    {
        Routiner.InvokeNextFrame(() =>
        {
            SoundSettingChanged.Invoke(SoundEnabled);
            MusicSettingChanged.Invoke(MusicEnabled);
            SoundChanged?.Invoke(SoundEnabled);
            MusicChanged?.Invoke(MusicEnabled);
        });
    }

    public void ToggleSound()
    {
        if (SoundEnabled)
        {
            PlayerPrefs.SetInt(SFX_ENABLED_KEY, 1);
        }
        else
        {
            PlayerPrefs.DeleteKey(SFX_ENABLED_KEY);
        }
        SoundSettingChanged.Invoke(SoundEnabled);
        SoundChanged?.Invoke(SoundEnabled);
    }

    public void ToggleMusic()
    {
        if (MusicEnabled)
        {
            PlayerPrefs.SetInt(MUSIC_ENABLED_KEY, 1);
        }
        else
        {
            PlayerPrefs.DeleteKey(MUSIC_ENABLED_KEY);
        }
        MusicSettingChanged.Invoke(MusicEnabled);
        MusicChanged?.Invoke(MusicEnabled);
    }
}