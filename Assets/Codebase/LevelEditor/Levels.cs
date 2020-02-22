#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEngine;

namespace LevelEditor.Sorting
{
    [Serializable]
    public class Levels : ScriptableObject
    {
        [SerializeField] public List<LevelData> AllLevels;
    }
}
#endif