using System.Collections.Generic;
using Crysberry.Audio.Editor;
using UnityEditor;
using UnityEngine;

namespace Crysberry.Audio
{
	[CustomEditor(typeof(AudioGroupDefinition))]
	public class AudioGroupDefinitionEditor : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			var audioGroup = target as AudioGroupDefinition;

			EditorGUI.BeginChangeCheck();
			
			EditorGUILayout.PropertyField(serializedObject.FindProperty ("Definitions"), true);
			EditorGUILayout.PropertyField(serializedObject.FindProperty ("MixerGroup"), true);
			
			audioGroup.UseCommonVolume = GUILayout.Toggle(audioGroup.UseCommonVolume, "Use common volume settings");
			if (audioGroup.UseCommonVolume)
			{
				DrawVolumeSettings(audioGroup);
				DrawPitchSection(audioGroup);
			}
			
			if (GUILayout.Button("Collect definitions in current folder + subfolders"))
			{
				var obj = Selection.activeObject;
				var path = AssetDatabase.GetAssetPath(obj);
				path = path.Substring(0, path.IndexOf(obj.name) - 1);

				var guids = AssetDatabase.FindAssets("t:AudioEffectDefinition", new []{path});

				var definitions = new List<AudioEffectDefinition>();
				foreach (string guid in guids)
				{
					var definitionPath = AssetDatabase.GUIDToAssetPath(guid);
					var asset = AssetDatabase.LoadAssetAtPath<AudioEffectDefinition>(definitionPath);
					definitions.Add(asset);
				}
				
				audioGroup.Definitions = definitions.ToArray();
				serializedObject.Update();
				Debug.LogFormat("{0}::Found {1} audio definitions at path {2}.", obj.name, audioGroup.Definitions.Length, path);
			}
			
			audioGroup.ApplyCommonSettingsIfNeeded();

			if (EditorGUI.EndChangeCheck())
			{
				EditorUtility.SetDirty(target);
				serializedObject.ApplyModifiedProperties();
			}
		}
		
		private void DrawPitchSection(AudioGroupDefinition audioEffectDefinition)
		{
			EditorGUILayout.Separator();
			EditorGUILayout.LabelField("Pitch settings", EditorStyles.boldLabel);
			audioEffectDefinition.UsesRandomPitch = EditorGUILayout.ToggleLeft("Use random pitch", audioEffectDefinition.UsesRandomPitch);

			if (audioEffectDefinition.UsesRandomPitch)
			{
				EditorGUILayout.LabelField("Minimal possible pitch:", audioEffectDefinition.PitchVarietyMin.ToString());
				EditorGUILayout.LabelField("Maximum possible pitch:", audioEffectDefinition.PitchVarietyMax.ToString());
				EditorGUILayout.MinMaxSlider(ref audioEffectDefinition.PitchVarietyMin,
					ref audioEffectDefinition.PitchVarietyMax, AudioEffectDefinitionEditor.minAudioPitchLimit, AudioEffectDefinitionEditor.maxAudioPitchLimit);
			}
			
		}
		
		public void DrawVolumeSettings(AudioGroupDefinition audioGroupDefinition)
		{
			EditorGUILayout.Separator();
			EditorGUILayout.LabelField("Volume settings", EditorStyles.boldLabel);
			EditorGUILayout.LabelField("Volume");
			audioGroupDefinition.Volume = EditorGUILayout.Slider(audioGroupDefinition.Volume, 0f, 1f);
			
			audioGroupDefinition.FadesIn = EditorGUILayout.ToggleLeft("Fades in", audioGroupDefinition.FadesIn);
			if (audioGroupDefinition.FadesIn)
			{
				EditorGUILayout.LabelField("Gains max volume at percentage:");
				audioGroupDefinition.FadeInEndPercentage = EditorGUILayout.Slider(audioGroupDefinition.FadeInEndPercentage, 0f, 1f);			
			}
			
			audioGroupDefinition.FadesOut = EditorGUILayout.ToggleLeft("Fades out", audioGroupDefinition.FadesOut);
			if (audioGroupDefinition.FadesOut)
			{
				EditorGUILayout.LabelField("Starts to lose volume at percentage:");
				var sliderStartValue = audioGroupDefinition.FadesIn ? audioGroupDefinition.FadeInEndPercentage : 0f;
				
				audioGroupDefinition.FadeOutBeginPercentage = EditorGUILayout.Slider(audioGroupDefinition.FadeOutBeginPercentage, sliderStartValue, 1f);
			}
			audioGroupDefinition.Is3DSound = EditorGUILayout.ToggleLeft("Is 3D sound", audioGroupDefinition.Is3DSound);
		}
	}
}

