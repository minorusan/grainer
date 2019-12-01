#if UNITY_EDITOR
using UnityEditor;
public class AssetsNameToLower : AssetPostprocessor
{
//    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
//    {
//        foreach (var item in importedAssets)
//        {
//            if (!item.Contains(".") || item.Contains(".cs"))
//            {
//                continue;
//            }
//            var asset = AssetDatabase.LoadMainAssetAtPath(item);
//            AssetDatabase.RenameAsset(item, asset.name.ToLower());
//        }
//    }
}
#endif
