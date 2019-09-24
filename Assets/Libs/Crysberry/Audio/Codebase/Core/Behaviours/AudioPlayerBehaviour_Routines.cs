using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Crysberry.Audio
{
	public partial class AudioPlayerBehavior
	{
		private void FadeAudioIfNeeded(AudioEffectDefinition definition)
		{
			if (definition.FadesIn)
			{
				StartCoroutine(FadeAudioIn(definition.FadeInEndPercentage, definition.Volume));
			}
			else
			{
				audioSource.volume = definition.Volume;
			}

			if (definition.FadesOut)
			{
				StartCoroutine(FadeAudioOut(definition.FadeOutBeginPercentage, definition.Volume));
			}

			if (definition.Loops)
			{
				StartCoroutine(ReinitializeFadeOnLoop(definition));
			}
		}

		private IEnumerator InvalidateDelayed(AudioEffectDefinition definition)
		{
			yield return new WaitForSeconds(definition.Clip.length);
			InvalidateIfNeeded(definition);
		}

		private IEnumerator ReinitializeFadeOnLoop(AudioEffectDefinition definition)
		{
			yield return new WaitForSeconds(audioSource.clip.length);
			StopAllCoroutines();
			FadeAudioIfNeeded(definition);
		}
		
		private IEnumerator FadeAudioIn(float fadeInEndPercentage, float maxVolume)
		{
			var waitForEndOfFrame = new WaitForEndOfFrame();
			var fadeInDuration = audioSource.clip.length * fadeInEndPercentage;
			var currentFadeTime = 0f;
			
			while (currentFadeTime <= fadeInDuration)
			{
				currentFadeTime += Time.deltaTime;
				audioSource.volume = Mathf.Lerp(0f, maxVolume, currentFadeTime / fadeInDuration);
				
				yield return waitForEndOfFrame;
			}
		}
		
		private IEnumerator FadeAudioOut(float fadeOutBeginPercentage, float maxVolume)
		{
			var waitForEndOfFrame = new WaitForEndOfFrame();
			var fadeOutStartTime = (audioSource.clip.length * fadeOutBeginPercentage);
			
			var volume = maxVolume;
			
			yield return new WaitForSeconds(fadeOutStartTime);
			
			var fadeOutDuration = audioSource.clip.length - fadeOutStartTime;
			var currentFadeTime = 0f;
			
			while (currentFadeTime <= fadeOutDuration)
			{
				currentFadeTime += Time.deltaTime;
				audioSource.volume = Mathf.Lerp(volume, 0f, currentFadeTime / fadeOutDuration);
				
				yield return waitForEndOfFrame;
			}
		}
	}
}

