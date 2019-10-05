﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class AreaHelper 
{
    private static Texture2D currentMap;
    private static Dictionary<Position, GameObject> cellCoordinates = new Dictionary<Position, GameObject>();

    static AreaHelper()
    {
        SceneManager.activeSceneChanged += OnSceneChanged;
    }

    private static void OnSceneChanged(Scene arg0, Scene arg1)
    {
        currentMap = null;
    }

    private static void GetTextureIfNeeded()
    {
        if (currentMap == null)
        {
            var areaInitializer = Object.FindObjectOfType<AreaInitializeBehaviour>();
            currentMap = areaInitializer.CurrentMap;
            cellCoordinates.Clear();
            var count = areaInitializer.TilesHub.childCount;
            for (int i = 0; i < count; i++)
            {
                var child = areaInitializer.TilesHub.GetChild(i);
                cellCoordinates.Add(child.position.ToPosition(), child.gameObject);
            }
        }
    }

    public static GameObject GetCell(Position position)
    {
        if (cellCoordinates != null && cellCoordinates.TryGetValue(position, out var gameObject))
        {
            return gameObject;
        }
        return null;
    }

    public static ColorDefinition GetDefinition(Vector3 position)
    {
        GetTextureIfNeeded();
        var point = position.ToPosition();
        if (point.X < 0 || point.X > currentMap.width || point.Y < 0 || point.Y >= currentMap.height)
        {
            return null;
        }

        var color = currentMap.GetPixel(point.X, point.Y);
        if (ColorMap.Instance.GetDefinition(color, out var colorDefinition))
        {
            return colorDefinition;
        }
        Debug.LogError($"No definition for position {position}");
        return null;
    }

    public static bool IsWalkable(Vector3 position)
    {
        var definition = GetDefinition(position);
        if (definition != null)
            return definition.IsWalkable;
        return false;
    }
}