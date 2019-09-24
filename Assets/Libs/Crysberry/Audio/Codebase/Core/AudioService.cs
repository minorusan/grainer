using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crysberry.Audio
{
	public enum AudioPriority
	{
		Normal, High
	}
	
	public class AudioService
	{
		private const string defaultPlayerName = "Player for definition <{0}>";
		private const string defaultStorageName = "Audio effects storage";
		private const int defaultAudioQueueCapacity = 100;
		private const int defaultSoundsPerSecond = 10;

		private int soundsPerSecond = defaultSoundsPerSecond;
		private int soundsPerLastSecond;
		private float previousPlayRequestTime;
		
		private List<AudioPlayerBehavior> activePlayers = new List<AudioPlayerBehavior>(defaultAudioQueueCapacity);
		private Transform audioEffectsStorage;

		public int SoundsPerSecond
		{
			get { return soundsPerSecond; }
			set { soundsPerSecond = value; }
		}

		public AudioPlayerBehavior Play(AudioEffectDefinition definition, AudioPriority priority = AudioPriority.Normal)
		{
			return Play(definition, Vector3.zero, priority);
		}

		public AudioPlayerBehavior Play(AudioEffectDefinition definition, Vector3 location,
			AudioPriority priority = AudioPriority.Normal)
		{
			var currentTime = Time.time;
			if ((currentTime - previousPlayRequestTime < 1f) && soundsPerLastSecond >= SoundsPerSecond && priority != AudioPriority.High)
			{
				return null;
			}

			if (currentTime - previousPlayRequestTime > 1f)
			{
				soundsPerLastSecond = 0;
			}

			previousPlayRequestTime = currentTime;
			soundsPerLastSecond++;
			
			InitIfNeeded();
			
			var audioPlayer = new GameObject(string.Format(defaultPlayerName, definition.name))
				.AddComponent<AudioPlayerBehavior>();
			audioPlayer.transform.SetParent(audioEffectsStorage);
			
			audioPlayer.PlayDefinition(definition);
			audioPlayer.transform.position = location;
			
			activePlayers.Add(audioPlayer);
			audioPlayer.DidEndPlaying += OnPlayerEndPlaying;
			
			return audioPlayer;
		}

		private void OnPlayerEndPlaying(AudioPlayerBehavior sender, AudioPlayerEventArgs args)
		{
			activePlayers.Remove(sender);
		}

		private void InitIfNeeded()
		{
			if (audioEffectsStorage == null)
			{
				var existingStorage = GameObject.Find(defaultStorageName);
				audioEffectsStorage = existingStorage != null ? existingStorage.transform : new GameObject(defaultStorageName).transform;
				
				GameObject.DontDestroyOnLoad(audioEffectsStorage.gameObject);
			}
		}

		public void Invalidate()
		{
			if (audioEffectsStorage != null && audioEffectsStorage.childCount > 0)
			{
				var leakedBehaviours = "AudioService::Upon invalidation, several non-loop AudioPlayers were detected, which can be a sign of a leak. Full list: \n";
				for (var i = 0; i < audioEffectsStorage.childCount; i++)
				{
					var child = audioEffectsStorage.GetChild(i);
					leakedBehaviours += child.name + "\n";
				}
				Debug.LogWarning(leakedBehaviours);
			}
		}	
	}
}

