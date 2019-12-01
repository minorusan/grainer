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
        var texture = (Texture2D) AssetDatabase.LoadAssetAtPath(level.levelTexturePath, typeof(Texture2D));
        if (level.levelTexture == null)
        {
            level.levelTexture = texture;
            EditorUtility.SetDirty(level);
        }
        
        level.levelTexture = texture;
        EditorGUILayout.ObjectField("Texture", texture , typeof(Texture2D), false);
        
        EditorGUI.EndDisabledGroup();
        
    }
}
