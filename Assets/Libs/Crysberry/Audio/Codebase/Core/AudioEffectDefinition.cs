using UnityEngine;
using UnityEngine.Audio;

namespace Crysberry.Audio
{
	[CreateAssetMenu(fileName = "New audio effect definition", menuName = "Crysberry/Audio/Effect")]
	public class AudioEffectDefinition : ScriptableObject
	{
		#region Public members

		public AudioGroupDefinition Group;
		public AudioClip AudioClip;
		public AudioMixerGroup MixerGroup;

		public bool UseRandom;
		public AudioClip[] RandomClips;

		public bool UsesRandomPitch;
		public float PitchVarietyMin = 1f;
		public float PitchVarietyMax = 1f;

		public bool AutokillOnEnd;
		public bool Loops;

		public float Volume = 1f;

		public bool FadesIn = true;
		public float FadeInEndPercentage = 0.3f;

		public bool FadesOut = true;
		public float FadeOutBeginPercentage = 0.7f;

		public bool Is3DSound;

		#endregion

		/// <summary>
		/// Return clip, according to definition settings
		/// </summary>
		public AudioClip Clip
		{
			get
			{
				return UseRandom ? RandomClips[Random.Range(0, RandomClips.Length)] : AudioClip;
			}
		}

		/// <summary>
		/// Returns pitch, according to definition settings
		/// </summary>
		public float Pitch
		{
			get { return UsesRandomPitch ? Random.Range(PitchVarietyMin, PitchVarietyMax) : 1f; }
		}
	}
}
