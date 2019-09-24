using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


namespace Crysberry.Audio
{
	[CreateAssetMenu(fileName = "New audio group definition", menuName = "Crysberry/Audio/Group")]
	public class AudioGroupDefinition : ScriptableObject
	{
		public AudioEffectDefinition[] Definitions;
		public AudioMixerGroup MixerGroup;

		#region Common volume settings

		public bool UseCommonVolume;
		
		public float Volume = 1f;

		public bool FadesIn = true;
		public float FadeInEndPercentage = 0.3f;

		public bool FadesOut = true;
		public float FadeOutBeginPercentage = 0.7f;
		public bool Is3DSound;
		
		public bool UsesRandomPitch;
		public float PitchVarietyMin = 1f;
		public float PitchVarietyMax = 1f;

		#endregion
		

		public void ApplyCommonSettingsIfNeeded()
		{
			foreach (var audioEffectDefinition in Definitions)
			{
				audioEffectDefinition.Group = this;
				audioEffectDefinition.MixerGroup = MixerGroup;
			}

			if (UseCommonVolume)
			{
				foreach (var audioEffectDefinition in Definitions)
				{
					audioEffectDefinition.UsesRandomPitch = UsesRandomPitch;
					audioEffectDefinition.PitchVarietyMin = PitchVarietyMin;
					audioEffectDefinition.PitchVarietyMax = PitchVarietyMax;

					audioEffectDefinition.FadesIn = FadesIn;
					audioEffectDefinition.FadeInEndPercentage = FadeInEndPercentage;
					audioEffectDefinition.FadesOut = FadesOut;
					audioEffectDefinition.FadeOutBeginPercentage = FadeOutBeginPercentage;

					audioEffectDefinition.Volume = Volume;
					audioEffectDefinition.Is3DSound = Is3DSound;
				}
			}
		}
	}
}