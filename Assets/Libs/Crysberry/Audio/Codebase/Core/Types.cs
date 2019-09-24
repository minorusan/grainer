using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crysberry.Audio
{
    public delegate void AudioPlayerEventHandler(AudioPlayerBehavior sender, AudioPlayerEventArgs args);

    public class AudioPlayerEventArgs : EventArgs
    {
        private readonly AudioEffectDefinition definition;
        private readonly Vector3 position;

        public Vector3 Position
        {
            get { return position; }
        }

        public AudioEffectDefinition Definition
        {
            get { return definition; }
        }

        public AudioPlayerEventArgs(AudioEffectDefinition definition, Vector3 position)
        {
            this.definition = definition;
            this.position = position;
        }
    }
}
