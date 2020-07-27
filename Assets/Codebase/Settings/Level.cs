using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

[Serializable]
public class Level : ScriptableObject
{
    [SerializeField] public int Number;
    public int version = 1;
    [SerializeField] public int Id;
    [HideInInspector] public Texture2D levelTexture;
    public string levelTexturePath;
#if UNITY_EDITOR
    [HideInInspector] public bool isDirty;
#endif

#if UNITY_EDITOR
    [MenuItem("Assets/Create/Grainer/Levels/Level", false, 1)]
    public static void CreateImageData()
    {
        Level data = ScriptableObject.CreateInstance<Level>();

        data.version = 1;
        var levels = Resources.LoadAll<Level>("Levels");
        ;
        data.Number = levels.Select(x => x.Number).Max();
        data.Id = levels.Select(x => x.Id).Max();
        data.Number++;
        data.Id++;
        int textureNumber = 0;

        foreach (var level in levels)
        {
            if (level.levelTexture != null)
            {
                if (textureNumber < int.Parse(level.levelTexture.name))
                {
                    textureNumber = int.Parse(level.levelTexture.name);
                }
            }
        }

        Texture2D tmpTexture = new Texture2D(10, 10);
        var pixels = tmpTexture.GetPixels();
        for (int i = 0; i < pixels.Length; i++)
        {
            pixels[i] = Color.white;
        }

        tmpTexture.SetPixels(pixels);
        textureNumber++;
        var bytes = tmpTexture.EncodeToPNG();
        var path = "Assets/Content/Resources/Textures/Maps/" + textureNumber + ".png";

        File.WriteAllBytes(path, bytes);

        AssetDatabase.Refresh();

        TextureImporter importer = TextureImporter.GetAtPath(path) as TextureImporter;
        importer.textureType = TextureImporterType.Default;
        importer.isReadable = true;

        UnityEngine.Object asset = AssetDatabase.LoadAssetAtPath(importer.assetPath, typeof(Texture2D));
        AssetDatabase.ImportAsset(importer.assetPath, ImportAssetOptions.ForceUpdate);
        if (asset)
        {
            EditorUtility.SetDirty(asset);
            Debug.Log("dirty");
        }
        else
        {
            importer.textureType = TextureImporterType.Advanced;
            Debug.Log("not dirty");
        }

        data.levelTexturePath = path;
        data.levelTexture =
            AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Content/Resources/Textures/Maps/" + textureNumber +
                                                     ".png");
        EditorUtility.SetDirty(data);
        AssetDatabase.CreateAsset(data, "Assets/Content/Resources/Levels/level_" + data.Number + ".asset");
        AssetDatabase.Refresh();
    }
#endif
}