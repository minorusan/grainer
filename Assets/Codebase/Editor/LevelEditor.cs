using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

[CustomEditor(typeof(Level))]
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
        staticHeight = EditorGUILayout.IntField("Weight:", height);
        staticwidth = EditorGUILayout.IntField("Weight:", width);
        if (GUILayout.Button("Create levelTexture"))
        {
              var texture2 = new Texture2D(staticwidth, staticHeight, TextureFormat.ARGB32, false);
             texture2.Apply();
            byte[] bytes = texture.EncodeToPNG();
            // Object.Destroy(texture);
            var sceneName = SceneManager.GetActiveScene().name;
            File.WriteAllBytes(
                Application.dataPath + "/_Game/Resources/MiniMap/LocationsGroundsMarkers/" + sceneName + "_Marker.png",
                bytes);
            string path = "Assets/_Game/Resources/MiniMap/LocationsGroundsMarkers/" + sceneName + "_Marker.png";
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            TextureImporter importer =
                AssetImporter.GetAtPath("Assets/_Game/Resources/MiniMap/LocationsGroundsMarkers/" + sceneName +
                                        "_Marker.png") as TextureImporter;
            importer.wrapMode = TextureWrapMode.Clamp;
            importer.textureType = TextureImporterType.Sprite;
            importer.mipmapEnabled = false;
            importer.maxTextureSize = 1024;
            importer.textureFormat = TextureImporterFormat.ETC_RGB4Crunched;
            importer.SaveAndReimport();
            AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
        }
    }
}