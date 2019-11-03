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
//            var levelHandler = new LevelHandler();
//            var levelInfo = levelHandler.GetLevelInfo(texture);
//            if (levelInfo.IsWorker)
//            {
//                Level asset = ScriptableObject.CreateInstance<Level>();
//                asset.minTurnsCount = levelInfo.MinimumTurns;
//                asset.readyToUse = true;
//                asset.levelTexturePath =assetPath;
//               var str = assetPath.Replace("Assets/Content/Textures/Maps/","");
//                 str = str.Replace(".png","");
//                AssetDatabase.CreateAsset(asset, "Assets/Content/Levels/Level_"+str+".asset");
//                AssetDatabase.SaveAssets();
//
//                EditorUtility.FocusProjectWindow();
//
//                Selection.activeObject = asset;
//            }
            //Do your stuff here
        }
    }
}
#endif