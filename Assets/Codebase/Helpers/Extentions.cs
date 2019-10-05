using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extentions 
{
    private static Vector3[] directions = {Vector3.zero, Vector3.left, Vector3.right, Vector3.forward, Vector3.back};

    public static string ToHexString(this Color color)
    {
        return ColorUtility.ToHtmlStringRGB(color);
    }

    public static Vector3 ToVector3(this MovementDirection direction)
    {
        return directions[(int)direction];
    }

    public static Position ToPosition(this Vector3 position)
    {
        return new Position(position.x, position.z);
    }

    public static bool IsWalkable(this Vector3 position)
    {
        return AreaHelper.IsWalkable(position);
    }

    public static GameObject ToCellGameObject(this Vector3 position)
    {
        return AreaHelper.GetCell(position.ToPosition());
    }

    public static ColorDefinition ToColorDefinition(this Vector3 position)
    {
        return AreaHelper.GetDefinition(position);
    }
}
