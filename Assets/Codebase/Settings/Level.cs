using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New level", menuName = "Grainer/Levels/Level")]
[Serializable]
public class Level : ScriptableObject
{
    public int version = 1;
    [SerializeField] public int Id;
    [HideInInspector] public Texture2D levelTexture;
    public string levelTexturePath;
    [SerializeField] public int Number;
#if UNITY_EDITOR
    public bool isDirty;
#endif
}