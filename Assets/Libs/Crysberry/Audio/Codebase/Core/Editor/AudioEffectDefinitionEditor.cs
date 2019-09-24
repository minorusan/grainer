using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Crysberry.Audio.Editor
{
	[CustomEditor(typeof(AudioEffectDefinition))]
	public class AudioEffectDefinitionEditor : UnityEditor.Editor
	{
		public const float minAudioPitchLimit = -3f;
		public const float maxAudioPitchLimit = 3f;
		
		public override void OnInspectorGUI()
		{
			var audioEffectDefiniton = target as AudioEffectDefinition;

			EditorGUI.BeginChangeCheck();
			if (audioEffectDefiniton.Group != null)
			{
				EditorGUILayout.PropertyField(serializedObject.FindProperty ("Group"), true);
			}
			else
			{
				EditorGUILayout.PropertyField(serializedObject.FindProperty ("MixerGroup"), true);
			}
				
			if(EditorGUI.EndChangeCheck())
				serializedObject.ApplyModifiedProperties();
			
			if (audioEffectDefiniton.Group == null || (audioEffectDefiniton.Group != null && !audioEffectDefiniton.Group.UseCommonVolume))
			{
				DrawAudioSection(audioEffectDefiniton);
				DrawPitchSection(audioEffectDefiniton);
				DrawLifecycleSettings(audioEffectDefiniton);
				DrawVolumeSettings(audioEffectDefiniton);
			}
			else
			{
				EditorGUILayout.LabelField("This definition settings are controlled by group.", EditorStyles.boldLabel);
			}
			
			EditorUtility.SetDirty(target);
		}

		private void DrawLifecycleSettings(AudioEffectDefinition audioEffectDefinition)
		{
			EditorGUILayout.Separator();
			EditorGUILayout.LabelField("Lifecycle settings", EditorStyles.boldLabel);
			audioEffectDefinition.Loops = EditorGUILayout.ToggleLeft("Loops", audioEffectDefinition.Loops);
			if (!audioEffectDefinition.Loops)
			{
				audioEffectDefinition.AutokillOnEnd = 
					EditorGUILayout.ToggleLeft("Destroys on end", audioEffectDefinition.AutokillOnEnd);
			}
			else
			{
				audioEffectDefinition.AutokillOnEnd = false;
			}
		}

		public void DrawVolumeSettings(AudioEffectDefinition audioEffectDefinition)
		{
			EditorGUILayout.Separator();
			EditorGUILayout.LabelField("Volume settings", EditorStyles.boldLabel);
			EditorGUILayout.LabelField("Volume");
			audioEffectDefinition.Volume = EditorGUILayout.Slider(audioEffectDefinition.Volume, 0f, 1f);
			
			audioEffectDefinition.FadesIn = EditorGUILayout.ToggleLeft("Fades in", audioEffectDefinition.FadesIn);
			if (audioEffectDefinition.FadesIn)
			{
				EditorGUILayout.LabelField("Gains max volume at percentage:");
				audioEffectDefinition.FadeInEndPercentage = EditorGUILayout.Slider(audioEffectDefinition.FadeInEndPercentage, 0f, 1f);			
			}
			
			audioEffectDefinition.FadesOut = EditorGUILayout.ToggleLeft("Fades out", audioEffectDefinition.FadesOut);
			if (audioEffectDefinition.FadesOut)
			{
				EditorGUILayout.LabelField("Starts to lose volume at percentage:");
				var sliderStartValue = audioEffectDefinition.FadesIn ? audioEffectDefinition.FadeInEndPercentage : 0f;
				
				audioEffectDefinition.FadeOutBeginPercentage = EditorGUILayout.Slider(audioEffectDefinition.FadeOutBeginPercentage, sliderStartValue, 1f);
			}
			audioEffectDefinition.Is3DSound = EditorGUILayout.ToggleLeft("Is 3D sound", audioEffectDefinition.Is3DSound);
		}

		private void DrawPitchSection(AudioEffectDefinition audioEffectDefinition)
		{
			EditorGUILayout.Separator();
			EditorGUILayout.LabelField("Pitch settings", EditorStyles.boldLabel);
			audioEffectDefinition.UsesRandomPitch = EditorGUILayout.ToggleLeft("Use random pitch", audioEffectDefinition.UsesRandomPitch);

			if (audioEffectDefinition.UsesRandomPitch)
			{
				EditorGUILayout.LabelField("Minimal possible pitch:", audioEffectDefinition.PitchVarietyMin.ToString());
				EditorGUILayout.LabelField("Maximum possible pitch:", audioEffectDefinition.PitchVarietyMax.ToString());
				EditorGUILayout.MinMaxSlider(ref audioEffectDefinition.PitchVarietyMin,
					ref audioEffectDefinition.PitchVarietyMax, minAudioPitchLimit, maxAudioPitchLimit);
			}
			
		}

		private void DrawAudioSection(AudioEffectDefinition audioEffectDefiniton)
		{
			EditorGUILayout.LabelField("Clip settings", EditorStyles.boldLabel);
			audioEffectDefiniton.UseRandom = EditorGUILayout.ToggleLeft("Use random", audioEffectDefiniton.UseRandom);
			EditorGUI.BeginChangeCheck();
			if (audioEffectDefiniton.UseRandom)
			{
				EditorGUILayout.PropertyField(serializedObject.FindProperty ("RandomClips"), true);
			}
			else
			{
				EditorGUILayout.PropertyField(serializedObject.FindProperty ("AudioClip"), true);	
			}
			
			if(EditorGUI.EndChangeCheck())
				serializedObject.ApplyModifiedProperties();
		}
	}
}

