#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public class MapsPostprocessor : AssetPostprocessor
{
    private const string mapsPath = "Content/Textures/Maps";

    private void OnPreprocessTexture()
    {
        if (assetPath.Contains(mapsPath))
        {
            TextureImporter textureImporter  = (TextureImporter)assetImporter;
            textureImporter.textureType = TextureImporterType.Default;
            textureImporter.mipmapEnabled = false;
            textureImporter.isReadable = true;
            textureImporter.textureCompression = TextureImporterCompression.Uncompressed;
            Debug.Log($"Preprocessed {assetPath}");
        }
    }

    private void OnPostprocessTexture(Texture2D texture)
    {
        if (assetPath.Contains(mapsPath))
        {
//            Level asset = ScriptableObject.CreateInstance<Level>();
//            asset.levelTexturePath = AssetDatabase.GetAssetPath(texture);
//            var str = asset.levelTexturePath.Replace("Assets/Content/Textures/Maps/", "");
//            str = str.Replace(".png", "");
//           
//            if (asset.levelTexture == null)
//            {
//                asset.levelTexture = texture;
//            }
//
//            EditorUtility.SetDirty(asset);
//            AssetDatabase.CreateAsset(asset, "Assets/Content/Resources/Levels/Level_" + str + ".asset");
//
//            AssetDatabase.SaveAssets();

            //Do your stuff here
        }
    }
}
#endif