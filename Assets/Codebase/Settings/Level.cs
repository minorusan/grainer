using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New level", menuName = "Grainer/Levels/Level")]
[Serializable]
public class Level : ScriptableObject
{
    [SerializeField] public int Number;
    public int version = 1;
    [SerializeField] public int Id;
    [HideInInspector] public Texture2D levelTexture;
    public string levelTexturePath;
#if UNITY_EDITOR
    [HideInInspector]public bool isDirty;
#endif
}