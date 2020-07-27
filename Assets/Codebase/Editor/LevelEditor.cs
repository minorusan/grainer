using System.IO;
using System.Linq;
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
        if (GUILayout.Button("Create level end edit"))
        {
            var so = ScriptableObject.CreateInstance<Level>();
            so.version = 1;
            var levels = Resources.LoadAll<Level>("Levels");;
            so.Number = levels.Select(x => x.Number).Max(); 
            var textureNumber = levels.Select(x => int.Parse(x.levelTexture.name) ).Max(); 
      
            Texture2D tmpTexture = new Texture2D(10,10);
      
            
            // CurrentLevel.levelTexture.Apply(false);
      
            var bytes = tmpTexture.EncodeToPNG();
            var path = "Assets/Content/Resources/Textures/Maps/"+textureNumber+".png";
            File.WriteAllBytes(path , bytes);
            so.name = "level_" + so.Number;
      
      
            EditorUtility.SetDirty(so);
            AssetDatabase.SaveAssets();
            
            PlayerPrefs.SetInt("EditLevel", so.Number);
            UnityEditor.EditorApplication.isPlaying = true    ;
            EditorSceneManager.OpenScene("Assets/Content/Scenes/level_editor.unity");
        } 
    }
}