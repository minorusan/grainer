using UnityEngine;

[CreateAssetMenu(fileName = "New level", menuName = "Grainer/Levels/Level")]
public class Level : ScriptableObject
{
    [HideInInspector] public Texture2D levelTexture;
    public string levelTexturePath;
}