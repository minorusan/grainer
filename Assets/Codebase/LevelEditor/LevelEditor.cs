#if UNITY_EDITOR
using System;
using System.IO;
using UnityEditor;
using UnityEditor.iOS.Xcode;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LevelEditor
{
    public class LevelEditor : MonoBehaviour
    {
        public Level CurrentLevel;
        [SerializeField] private GameObject NewLevelPanel;

        private void Awake()
        {
            if (PlayerPrefs.HasKey("EditLevel"))
            {
                var levelNumber = PlayerPrefs.GetInt("EditLevel", 1);
                //PlayerPrefs.DeleteKey("EditLevel");
                CurrentLevel = Resources.Load<Level>("Levels/level_" + levelNumber);
                FindObjectOfType<GridBuilder>().BuildLevel(CurrentLevel.levelTexture);
            }
            else
            {
                //NewLevelPanel.SetActive = true;
            }
        }

        public void ChangeSize()
        {
            var resizeLevelBehaviour = FindObjectOfType<ResizeLevelBehaviour>();
            Texture2D tmpTexture = new Texture2D(CurrentLevel.levelTexture.width,CurrentLevel.levelTexture.height);
            tmpTexture.SetPixels(CurrentLevel.levelTexture.GetPixels());
            // var newWidth = int.Parse(resizeLevelBehaviour.width.text) >= CurrentLevel.levelTexture.width
            //     ? 
            //      CurrentLevel.levelTexture.width:int.Parse(resizeLevelBehaviour.width.text);
            // var newHeight = int.Parse(resizeLevelBehaviour.height.text) >= CurrentLevel.levelTexture.height
            //     ? 
            //      CurrentLevel.levelTexture.height:int.Parse(resizeLevelBehaviour.height.text);
            // var pixels = CurrentLevel.levelTexture.GetPixels(0, 0, newWidth,
            //     newHeight);
            CurrentLevel.levelTexture.Resize(int.Parse(resizeLevelBehaviour.width.text),
                int.Parse(resizeLevelBehaviour.height.text));


            for (int i = 0; i < CurrentLevel.levelTexture.width; i++)
            {
                for (int j = 0; j < CurrentLevel.levelTexture.height; j++)
                {
                    if (i<=tmpTexture.width&&j<=tmpTexture.height)
                    {
                        CurrentLevel.levelTexture.SetPixel(i,j,tmpTexture.GetPixel(i,j));
                    }
                }
            }
            
            CurrentLevel.levelTexture.Apply(false);
            EditorUtility.SetDirty(CurrentLevel);
            AssetDatabase.SaveAssets();
            var bytes = CurrentLevel.levelTexture.EncodeToPNG();
            var path = AssetDatabase.GetAssetPath(CurrentLevel.levelTexture);
            File.WriteAllBytes(path , bytes);
            SceneManager.LoadScene("Assets/Content/Scenes/level_editor.unity");
        }

        public void ChangeLevelSize(int width, int height)
        {
            var texture = CurrentLevel.levelTexture;
            texture.Resize(width, height);
        }

        public void Save(bool increaseVersion)
        {
            var cells = FindObjectsOfType<Cell>();
            foreach (var cell in cells)
            {
                CurrentLevel.levelTexture.SetPixel(cell.cellPosY,cell.cellPosX,cell.cellColor);
            }
            CurrentLevel.levelTexture.Apply(false);
            if (increaseVersion)
            {
                CurrentLevel.version++;
            }
            EditorUtility.SetDirty(CurrentLevel);
            AssetDatabase.SaveAssets();
            var bytes = CurrentLevel.levelTexture.EncodeToPNG();
            var path = AssetDatabase.GetAssetPath(CurrentLevel.levelTexture);
            File.WriteAllBytes(path , bytes);
            PlayerPrefs.DeleteKey("EditLevel");
        }

    }
}
#endif