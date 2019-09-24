using UnityEngine;

namespace Crysberry.Audio
{
	public partial class AudioPlayerBehavior : MonoBehaviour
	{
		public event AudioPlayerEventHandler DidBeginPlaying = delegate(AudioPlayerBehavior sender, AudioPlayerEventArgs args) {  };
		public event AudioPlayerEventHandler DidEndPlaying = delegate(AudioPlayerBehavior sender, AudioPlayerEventArgs args) {  };

		private bool invalidated;
		private AudioSource audioSource;

		private void Awake()
		{
			InitializeIfNeeded();
		}

		public void PlayDefinition(AudioEffectDefinition definition)
		{
			InitializeIfNeeded();
			
			audioSource.clip = definition.Clip;
			audioSource.pitch = definition.Pitch;
			audioSource.loop = definition.Loops;
			audioSource.spatialBlend = definition.Is3DSound ? 1f : 0f;
			
			if (definition.AutokillOnEnd)
			{
				InvalidateIfNeeded(definition);
				Destroy(gameObject, audioSource.clip.length);
			}
			else
			{
				StartCoroutine(InvalidateDelayed(definition));
			}
			
			audioSource.Play();
			DidBeginPlaying(this, new AudioPlayerEventArgs(definition, transform.position));
			
			FadeAudioIfNeeded(definition);
		}

		private void InitializeIfNeeded()
		{
			audioSource = GetComponent<AudioSource>();
			if (audioSource == null)
			{
				audioSource = gameObject.AddComponent<AudioSource>();
				audioSource.playOnAwake = false;
			}
		}

		private void InvalidateIfNeeded(AudioEffectDefinition definition)
		{
			if (!invalidated)
			{
				DidEndPlaying(this, new AudioPlayerEventArgs(definition, transform.position));
				invalidated = true;
			}
		}
	}
}