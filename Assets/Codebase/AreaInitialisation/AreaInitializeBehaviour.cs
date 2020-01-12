using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class AreaInitializeBehaviour : MonoBehaviour
{
    private Texture2D currentMap;
    public bool DebugMode;
    public CameraFieldOfViewBehaviour Animation;
    public Texture2D DebugMap;
    public Transform CameraOffset;
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
            var map = LevelsHistory.GetLevelMap();
            if (map == null)
            {
                DebugMode = true;
                Start();
            }
            else
            {
                InitializeArea(LevelsHistory.GetLevelMap());
                DebugMap = LevelsHistory.GetLevelMap();
            }
        }
    }

    public void InitializeArea(Texture2D texture)
    {
        currentMap = texture;
        var textureWidth = texture.width;
        var textureHeight = texture.height;
        var colorMap = ColorMap.Instance;
        var color = Color.white;

        var cameraPosition = CameraOffset.transform.position;
        cameraPosition.x = TilesHub.transform.position.x + textureWidth * 0.5f;
        cameraPosition.z = TilesHub.transform.position.z + textureHeight * 0.5f;
        CameraOffset.transform.position = cameraPosition;
        Camera.main.transform.DOMove(cameraPosition, 2f);
        Animation.DoFieldOfView(new Vector2(textureWidth, textureHeight));
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

                    var setMaterialAsChessboardBehaviour = tile.GetComponent<SetMaterialAsChessboardBehaviour>();
                    if (setMaterialAsChessboardBehaviour != null)
                    {
                        setMaterialAsChessboardBehaviour.SetMaterial(i, j);
                    }
                    
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