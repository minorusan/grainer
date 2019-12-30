using System;
using UnityEngine;

public class SoundtrackBehaviour : MonoBehaviour
{
    public AudioSource Ambient;
    public AudioSource Music;

    private void Awake()
    {
        if (!AudioSettingsBehaviour.MusicEnabled)
        {
            if (Music != null)
            {
                Music.enabled = false;
            }
        }
        
        if (!AudioSettingsBehaviour.SoundEnabled)
        {
            if (Ambient != null)
            {
                Ambient.enabled = false;
            }
        }
        
        AudioSettingsBehaviour.SoundChanged += delegate(bool b)
        {
            if (Ambient != null)
            {
                Ambient.enabled = b;
            }
            
        };
        AudioSettingsBehaviour.MusicChanged += delegate(bool b)
        {
            if (Music != null)
            {
                Music.enabled = b;
            }
        };
    }
}
