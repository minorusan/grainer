using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extentions 
{
    public static string ToHexString(this Color color)
    {
        return ColorUtility.ToHtmlStringRGB(color);
    }
}
