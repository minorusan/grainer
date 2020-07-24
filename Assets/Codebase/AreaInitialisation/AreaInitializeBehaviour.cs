﻿using System;
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

    private void OnDrawGizmos()
    {
        if (DebugMode)
        {
            foreach (var blockedCell in AreaHelper.BlockedCells)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawCube(blockedCell, Vector3.one);
            }
        }
    }

    private void Start()
    {
#if UNITY_EDITOR
        if (DebugMode)
        {
            InitializeArea(DebugMap);
        }
        else
        {
            var map = LevelsStorage.LevelMapForLevelNumber(AppState.GameplayLevelNumber);
            if (map == null)
            {
                DebugMode = true;
                Start();
            }
            else
            {
#endif
                InitializeArea(LevelsStorage.LevelMapForLevelNumber(AppState.GameplayLevelNumber));
                DebugMap = LevelsStorage.LevelMapForLevelNumber(AppState.GameplayLevelNumber);
#if UNITY_EDITOR
            }
        }
#endif
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
                    
                    if (definition.name.ToLower().Contains("entrance"))
                    {
                        AreaHelper.SetStartCell(tile.transform.position);
                    }
                    
                    if (definition.name.ToLower().Contains("exit"))
                    {
                        AreaHelper.SetEndCell(tile.transform.position);
                    }
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