using System;
using UnityEngine;

public class RefreshLevelsDataBehaviour : MonoBehaviour
{
    private void OnEnable()
    {
        LevelsHistory.RefreshLevelsDatabase(null, null);
    }
}
