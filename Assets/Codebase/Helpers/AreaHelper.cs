using UnityEngine;
using UnityEngine.SceneManagement;

public static class AreaHelper 
{
    private static Texture2D currentMap;

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
            currentMap = Object.FindObjectOfType<AreaInitializeBehaviour>().CurrentMap;
        }
    }

    public static bool IsWalkable(Vector3 position)
    {
        GetTextureIfNeeded();
        var point = position.ToPosition();
        if (point.X < 0 || point.X > currentMap.width || point.Y < 0 || point.Y >= currentMap.height)
        {
            return false;
        }

        var color = currentMap.GetPixel(point.X, point.Y);
        if (ColorMap.Instance.GetDefinition(color, out var colorDefinition))
        {
            return colorDefinition.IsWalkable;
        }
        Debug.LogError($"No definition for position {position}");
        return false;
    }
}