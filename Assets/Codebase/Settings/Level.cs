using UnityEngine;

[CreateAssetMenu(fileName = "New level", menuName = "Grainer/Levels/Level")]
public class Level : ScriptableObject
{
    [HideInInspector] public bool isUsed;
    [HideInInspector] public Texture2D levelTexture;
  public string levelTexturePath;
    public int maxTurnsCount;
    public int minTurnsCount;
    [HideInInspector] public bool readyToUse;
}