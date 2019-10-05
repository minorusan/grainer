using UnityEngine;
using System.Linq;
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
    public bool IsWalkable => speedCoefitient > 0f;
    public EventDefinition[] Events => events;

    public GameObject GetPrefab()
    {
        return prefabs[Random.Range(0, prefabs.Length)];
    }

    public void InvokeEvents(GameObject cell, CellEventType type)
    {
        if (events != null && events.Length > 0)
        {
            var query = events.ToList().Where(x => x != null && x.Type == type);
            if (query != null)
            {
                var validEvents = query.ToList();
                validEvents.ForEach(x => x.Invoke(cell));
            }
        }
    }
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