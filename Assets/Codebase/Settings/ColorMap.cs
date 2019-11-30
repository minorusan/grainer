using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "New color map", menuName = "Grainer/Colors/ColorMap")]
public class ColorMap : ScriptableObject
{
    private static Dictionary<Color, ColorDefinition> colorToDefinitionMap = new Dictionary<Color, ColorDefinition>();
    private static ColorMap instance;

    public static ColorMap Instance
    {
        get
        {
            if (instance != null)
            {
                return instance;
            }
            else
            {
                instance = Resources.LoadAll<ColorMap>("Settings").FirstOrDefault();
                return instance;
            }
        }
    }

    [SerializeField] private ColorDefinition[] colors;
    public ColorDefinition[] Colors => colors;

    public bool GetDefinition(Color color, out ColorDefinition definition)
    {
        if (colorToDefinitionMap.ContainsKey(color))
        {
            definition = colorToDefinitionMap[color];
            return true;
        }
        definition = colors.FirstOrDefault(col => col.Color.ToHexString() == color.ToHexString());
        colorToDefinitionMap.Add(color, definition);
        return definition != null;
    }

    private void OnEnable()
    {
        var colorMaps = Resources.LoadAll<ColorMap>("Settings");
        if (colorMaps.Length == 0 || colorMaps.Any(x => x != this))
        {
            Debug.LogError("You should create color map in Resources/Settings");
            return;
        }

        if (colorMaps.Length > 1)
        {
            Debug.LogError("There should be only one color map");
        }
    }

    public void GetColors()
    {
        colors = Resources.LoadAll<ColorDefinition>("Settings");
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(ColorMap))]
public class ColorMapEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Gather colors"))
        {
            var targ = target as ColorMap;
            targ.GetColors();
            EditorUtility.SetDirty(targ);
        }
    }
}
#endif