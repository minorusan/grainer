using System;
using UnityEngine;

public class LevelsViewBehaviour : MonoBehaviour
{
    public LevelPreviewBehaviour Prefab;
    private void OnEnable()
    {
        transform.ClearChildren();
        var levelPrefabs = Resources.LoadAll<Level>("Levels");
        for (int i = 0; i < levelPrefabs.Length; i++)
        {
            var newInstance = Instantiate(Prefab, transform);
            newInstance.Init(i);
        }
    }

    private void OnDisable()
    {
        transform.ClearChildren();
    }
}
