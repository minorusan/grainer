using System;
using UnityEngine;
using System.Linq;
using Random = UnityEngine.Random;
#if UNITY_EDITOR
using UnityEditor;

#endif

[Serializable]
public struct PrefabCandidate
{
    [Range(0f, 1f)]
    public float ChanceTreshold;
    public GameObject Prefab;
}

[CreateAssetMenu(fileName = "New color definition", menuName = "Grainer/Colors/Color")]
public class ColorDefinition : ScriptableObject
{
    [SerializeField] private Color color = UnityEngine.Color.white;

    [SerializeField] [Tooltip("Set 0 to define obstacle")]
    private float speedCoefitient;

    [SerializeField] private EventDefinition[] events;
    [SerializeField] private PrefabCandidate[] prefabs;


    public Color Color => color;
    public float SpeedCoefitient => speedCoefitient;
    public bool IsWalkable => speedCoefitient > 0f;
    public EventDefinition[] Events => events;

    public bool IsObjective;

    public GameObject GetPrefab()
    {
        var value = Random.value;
        var candidates = prefabs.Where(x => x.ChanceTreshold >= value).ToArray();
        return candidates[Random.Range(0, candidates.Length)].Prefab;
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (prefabs.Length == 1)
        {
            prefabs[0].ChanceTreshold = 1f;
            EditorUtility.SetDirty(this);
        }
        var candidate = prefabs.FirstOrDefault(x => x.ChanceTreshold >= 1f);
        if (candidate.Prefab == null)
        {
            Debug.LogError("Has to be at least one prefab with 100% chance treshold");
        }
    }
#endif
   

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