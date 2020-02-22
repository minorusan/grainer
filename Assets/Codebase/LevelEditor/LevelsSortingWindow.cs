#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace LevelEditor.Sorting
{
    internal class LevelsSortingWindow : EditorWindow
    {
        private const int TOP_PADDING = 2;

        private Dictionary<int, Level> ALLlevels;

        private Levels levelsSO;
        private SerializedObject m_AnimalsSO;
        private ReorderableList m_ReorderableList;
        private ReorderableList reorderList;
        private Vector2 scrollPosition;
        private SerializedObject serializedObject;
        private SerializedProperty serializedPropertyMyInt;

        private ReorderableList supportedLangauageList;
        public List<LevelData> SupportedLangauageList;

        [MenuItem("Grainer/Levels sorting")]
        public static void ShowWindow()
        {
            GetWindow(typeof(LevelsSortingWindow));
        }

        private void OnEnable()
        {
            Init();
        }

        private void OnFocus()
        {
            Init();
        }

        private void Init()
        {
            levelsSO = CreateInstance<Levels>();
            levelsSO.AllLevels = new List<LevelData>();
            var levelScriptableObjects = Resources.LoadAll<Level>("Levels");

            ALLlevels = new Dictionary<int, Level>();

            foreach (var levelScriptableObject in levelScriptableObjects)
            {
                levelsSO.AllLevels.Add(new LevelData(levelScriptableObject.Id, levelScriptableObject.Number,
                    levelScriptableObject));
                ALLlevels.Add(levelScriptableObject.Id, levelScriptableObject);
            }

            levelsSO.AllLevels.Sort((p1, p2) => p1.Number.CompareTo(p2.Number));
            //serializedObject = new UnityEditor.SerializedObject(Resources.Load<Levels>("levels"));
            var animals = levelsSO;
            if (animals)
            {
                m_AnimalsSO = new SerializedObject(animals);
                m_ReorderableList = new ReorderableList(m_AnimalsSO, m_AnimalsSO.FindProperty("AllLevels"), true, false,
                    false, false);
                m_ReorderableList.drawHeaderCallback = rect => EditorGUI.LabelField(rect, "levels");


                m_ReorderableList.drawElementCallback = (rect, index, isActive, isFocused) =>
                {
                    var element = m_ReorderableList.serializedProperty.GetArrayElementAtIndex(index);
                    var number = element.FindPropertyRelative("Number");
                    var id = element.FindPropertyRelative("ID");
                    rect.y += TOP_PADDING;
                    rect.height = EditorGUIUtility.singleLineHeight;
                    var newIndex = index + 1;
                    EditorGUI.LabelField(new Rect(rect.x, rect.y, 200, EditorGUIUtility.singleLineHeight),
                        "Number: " + /*number.intValue */newIndex + " Id: " + id.intValue);
                };

                m_ReorderableList.onReorderCallbackWithDetails = (list, index, newIndex) => { reorderList = list; };
            }
        }

        private void OnGUI()
        {
            if (GUILayout.Button("SAVE"))
            {
                if (reorderList == null)
                {
                    return;
                }

                for (var i = 0; i < reorderList.count; i++)
                {
                    var sdf = reorderList.serializedProperty.GetArrayElementAtIndex(i);
                    var ind = i + 1;

                    if (ind != sdf.FindPropertyRelative("Number").intValue)
                    {
                        var id = sdf.FindPropertyRelative("ID");
                        ALLlevels[id.intValue].Number = ind;
                        ALLlevels[id.intValue].isDirty = true;

                        Debug.LogError("Переместился. Был " + sdf.FindPropertyRelative("Number").intValue + " Стал " +
                                       ind + " id " + id.intValue);
                    }
                }


                foreach (var llevelsValue in ALLlevels.Values)
                {
                    if (llevelsValue.isDirty)
                    {
                        var asset = CreateInstance<Level>();
                        asset.levelTexturePath = AssetDatabase.GetAssetPath(llevelsValue.levelTexture);
                        var str = asset.levelTexturePath.Replace("Assets/Content/Resources/Textures/Maps/", "");
                        str = str.Replace(".png", "");
                        if (asset.levelTexture == null)
                        {
                            asset.levelTexture = llevelsValue.levelTexture;
                        }

                        asset.Id = llevelsValue.Id;
                        asset.Number = llevelsValue.Number;

                        EditorUtility.SetDirty(asset);
                        AssetDatabase.CreateAsset(asset,
                            "Assets/Content/Resources/Levels/level_" + llevelsValue.Number + ".asset");

                        AssetDatabase.SaveAssets();
                    }
                }
            }

            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.BeginVertical();

            scrollPosition =
                GUI.BeginScrollView(new Rect(10, 0, 300, m_ReorderableList.elementHeight * m_ReorderableList.count),
                    scrollPosition,
                    new Rect(0, 0, 0, m_ReorderableList.elementHeight * (m_ReorderableList.count * 1.5f)));

            if (m_AnimalsSO != null)
            {
                m_AnimalsSO.Update();
                m_AnimalsSO.ApplyModifiedProperties();
                EditorUtility.SetDirty(levelsSO);
                m_ReorderableList.DoLayoutList();
            }

            GUI.EndScrollView();


            EditorGUILayout.EndVertical();
        }
    }


    [Serializable]
    public class LevelData
    {
        public int ID;
        public Level LevelPrefab;
        public int Number;

        public LevelData(int id, int number, Level levelPrefab)
        {
            ID = id;
            Number = number;
            LevelPrefab = levelPrefab;
        }
    }
}
#endif
