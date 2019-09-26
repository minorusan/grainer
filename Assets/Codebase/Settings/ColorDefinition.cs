using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

#endif

[CreateAssetMenu(fileName = "New color definition", menuName = "Grainer/Colors/Color")]
public class ColorDefinition : ScriptableObject
{
    [SerializeField] private Color color = UnityEngine.Color.white;

    [SerializeField] [Tooltip("Set 0 to define obstacle")]
    private float speedCoefitient;

    [SerializeField] private EventDefinition[] events;
    [SerializeField] private GameObject[] prefabs;


    public Color Color => color;
    public float SpeedCoefitient => speedCoefitient;
    public bool IsWalkable => Mathf.Approximately(speedCoefitient, 0f);
    public EventDefinition[] Events => events;
    public GameObject[] Prefabs => prefabs;
}

#if UNITY_EDITOR
[CustomEditor(typeof(ColorDefinition))]
public class ColorEditor:Editor
{
    public override void OnInspectorGUI()
    {
        var targ = target as ColorDefinition;
        EditorGUILayout.TextField("HEX:", targ.Color.ToHexString());
        base.OnInspectorGUI();
    }
}

#endif