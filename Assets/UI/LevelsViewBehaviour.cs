using System;
using UnityEngine;

public class LevelsViewBehaviour : MonoBehaviour
{
    public LevelPreviewBehaviour Prefab;
    private void OnEnable()
    {
        transform.ClearChildren();
        for (int i = 1; i < LevelsStorage.LastLevelNumber; i++)
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
