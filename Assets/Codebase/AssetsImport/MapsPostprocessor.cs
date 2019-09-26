using System.Collections;
using System.Collections.Generic;
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
            Debug.Log($"Preprocessed {assetPath}");
        }
    }
}
