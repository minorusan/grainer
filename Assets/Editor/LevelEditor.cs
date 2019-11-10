using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(Level))]
public class LevelEditor : Editor 
{
    public override void OnInspectorGUI()
    {
        Level level = (Level) target;
       
        EditorGUI.BeginDisabledGroup(true);
        DrawDefaultInspector();
//        if (level.readyToUse)
//        {
//            EditorGUILayout.Toggle("Is used", level.isUsed );
//        }
//        else
//        {
//            EditorGUILayout.Toggle("Ready to use", level.readyToUse );
//        }
     
        EditorGUILayout.ObjectField("Texture", (Texture2D)AssetDatabase.LoadAssetAtPath(level.levelTexturePath, typeof(Texture2D)), typeof(Texture2D), false);

        EditorGUI.EndDisabledGroup();
        
    }
}
