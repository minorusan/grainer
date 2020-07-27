using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

[CustomEditor(typeof(Level)),CanEditMultipleObjects]
public class LevelEditor : Editor
{
    int height = 13;
    int width = 18;
    static int staticHeight;
    static int staticwidth;
    public override void OnInspectorGUI()
    {
        var level = (Level) target;

        EditorGUI.BeginDisabledGroup(true);
        DrawDefaultInspector();

        var texture = (Texture2D) AssetDatabase.LoadAssetAtPath(level.levelTexturePath, typeof(Texture2D));
        
        if (level.levelTexture == null)
        {
            level.levelTexture = texture;
            EditorUtility.SetDirty(level);
        }

        level.levelTexture = texture;
        EditorGUILayout.ObjectField("Texture", texture, typeof(Texture2D), false);

        EditorGUI.EndDisabledGroup();
        if (GUILayout.Button("Edit"))
        {
            PlayerPrefs.SetInt("EditLevel", level.Number);
            UnityEditor.EditorApplication.isPlaying = true    ;
            EditorSceneManager.OpenScene("Assets/Content/Scenes/level_editor.unity");
        } 
    }
}