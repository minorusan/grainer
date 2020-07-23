using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class AreaHelper 
{
    private static Texture2D currentMap;
    public static int ObjectivesCount { get; private set; }
    private static Dictionary<Position, GameObject> cellCoordinates = new Dictionary<Position, GameObject>();
    private static HashSet<Position> blockedCells = new HashSet<Position>();
    private static Dictionary<Position, Vector3> blockedCellsList = new Dictionary<Position, Vector3>();
    public static List<Vector3> BlockedCells => blockedCellsList.Values.ToList();
    

    static AreaHelper()
    {
        SceneManager.activeSceneChanged += OnSceneChanged;
    }

    private static void OnSceneChanged(Scene arg0, Scene arg1)
    {
        blockedCells.Clear();
        ObjectivesCount = 0;
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

    public static void SetObjectivesCount(int count)
    {
        ObjectivesCount = count;
        Debug.Log($"AreaHelper::Objectives count is {ObjectivesCount}");
    }

    public static void SetWalkable(Vector3 position, bool walkable)
    {
        if (walkable)
        {
            blockedCells.Remove(position.ToPosition());
            blockedCellsList.Remove(position.ToPosition());
        }
        else
        {
            blockedCells.Add(position.ToPosition());
            if (!blockedCellsList.ContainsKey(position.ToPosition()))
            {
                blockedCellsList.Add(position.ToPosition(), position);
            }
        }
    }

    public static GameObject GetCell(Position position)
    {
        GetTextureIfNeeded();
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

    public static Vector3 NearestWalkablePosition(Vector3 position)
    {
        var neighbours = new[]
        {
            position + Vector3.back, position + Vector3.forward, position + Vector3.left, position + Vector3.right
        };

        foreach (var neighbour in neighbours)
        {
            if (neighbour.IsWalkable())
            {
                return neighbour;
            }
        }
        Debug.LogError("No walkable cell at the start");
        return Vector3.zero;
    }

    public static bool IsWalkable(Vector3 position)
    {
        if (blockedCells.Contains(position.ToPosition()))
        {
            return false;
        }
        var definition = GetDefinition(position);
        if (definition != null)
            return definition.IsWalkable;
        return false;
    }
}