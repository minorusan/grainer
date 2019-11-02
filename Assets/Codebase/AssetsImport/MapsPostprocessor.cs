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
            Debug.Log($"Preprocessed {assetPath}");
        }
    }

    private void OnPostprocessTexture(Texture2D texture)
    {
        if (assetPath.Contains(mapsPath))
        {
            //Do your stuff here
        }
    }
}
#endif