using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New level", menuName = "Grainer/Levels/Level")]
[Serializable]
public class Level : ScriptableObject
{
    [SerializeField] public int Id;
#if UNITY_EDITOR
    public bool isDirty;
#endif
    [HideInInspector] public Texture2D levelTexture;
    public string levelTexturePath;
    [SerializeField] public int Number;
}