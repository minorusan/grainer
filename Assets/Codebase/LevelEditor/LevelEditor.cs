#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEngine;

namespace LevelEditor
{
    public class LevelEditor : MonoBehaviour
    {
        private Level CurrentLevel;

        private void Awake()
        {
            if (PlayerPrefs.HasKey("EditLevel"))
            {
                var levelNumber = PlayerPrefs.GetInt("EditLevel", 1);
                PlayerPrefs.DeleteKey("EditLevel");
                CurrentLevel = Resources.Load<Level>("Levels/level_" + levelNumber);
                FindObjectOfType<GridBuilder>().BuildLevel(CurrentLevel.levelTexture);
            }
        }

        public void Save()
        {
            var cells = FindObjectsOfType<Cell>();
            foreach (var cell in cells)
            {
                CurrentLevel.levelTexture.SetPixel(cell.cellPosY,cell.cellPosX,cell.cellColor);
            }
            CurrentLevel.levelTexture.Apply(false);
            CurrentLevel.version++;
            EditorUtility.SetDirty(CurrentLevel);
            AssetDatabase.SaveAssets();
            var bytes = CurrentLevel.levelTexture.EncodeToPNG();
            var path = AssetDatabase.GetAssetPath(CurrentLevel.levelTexture);
            File.WriteAllBytes(path , bytes);
        }

    }
}
#endif