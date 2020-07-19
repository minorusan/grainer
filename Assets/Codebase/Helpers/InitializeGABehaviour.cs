using System.Collections;
using System.Collections.Generic;
using GameAnalyticsSDK;
using UnityEngine;

public class InitializeGABehaviour : MonoBehaviour
{
    private static bool initialized;
    void Start()
    {
        if (!initialized)
        {
            GameAnalytics.Initialize();
            initialized = true;
        }
    }
}
