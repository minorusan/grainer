using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LevelHandler))]
public class LevelHandlerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        LevelHandler myScript = (LevelHandler)target;
        if(GUILayout.Button("Start level testing"))
        {
            myScript.StartTest();
        }
        if(GUILayout.Button("Stop level testing"))
        {
            myScript.DisableTest();
        }
        if(GUILayout.Button("Create level assets"))
        {
            myScript.CreateLevelAssets();
        }
        
        if(GUILayout.Button("Populate array from Maps"))
        {
            myScript.texture2DArray = Resources.LoadAll<Texture2D>("Textures/Maps");
            EditorUtility.SetDirty(target);
        }
    }
}