using System.Collections.Generic;
using UnityEngine;

public class AreaInitializeBehaviour : MonoBehaviour
{
    private Texture2D currentMap;
    public bool DebugMode;
    public Texture2D DebugMap;
    public Transform TilesHub;

    public Texture2D CurrentMap => currentMap;

    private void Start()
    {
        if (DebugMode)
        {
            InitializeArea(DebugMap);
        }
        else
        {
            InitializeArea(LevelsHistory.GetLevelMap());
            DebugMap = LevelsHistory.GetLevelMap();
        }
    }

    public void InitializeArea(Texture2D texture)
    {
        currentMap = texture;
        var textureWidth = texture.width;
        var textureHeight = texture.height;
        var colorMap = ColorMap.Instance;
        var color = Color.white;

        int objectivesCount = 0;
        for (int i = 0; i < textureWidth; i++)
        {
            for (int j = 0; j < textureHeight; j++)
            {
                color = texture.GetPixel(i, j);
                if (colorMap.GetDefinition(color, out var definition))
                {
                    if (definition.IsObjective)
                    {
                        objectivesCount++;
                    }
                    var prefab = definition.GetPrefab();
                    var tile = Instantiate(prefab, TilesHub);
                    tile.transform.localPosition = new Vector3(i, 0f, j);
                }
                else
                {
                    Debug.LogWarning($"No definition found {color.ToHexString()}");
                }
            }
        }
        AreaHelper.SetObjectivesCount(objectivesCount);
    }
}