using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowsBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        OptimizatonBehaviour.OptimizationLevelChanged += OptimizatonBehaviourOnOptimizationLevelChanged;
    }
    
    void OnDisable()
    {
        OptimizatonBehaviour.OptimizationLevelChanged -= OptimizatonBehaviourOnOptimizationLevelChanged;
    }

    private void OptimizatonBehaviourOnOptimizationLevelChanged(int obj)
    {
        var quality = 3 - obj;
        QualitySettings.shadows = (ShadowQuality) quality;
    }
}
