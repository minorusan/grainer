using UnityEngine;

public class AntializingBehaviour : MonoBehaviour
{
    void Start()
    {
        OptimizatonBehaviour.OptimizationLevelChanged += OptimizatonBehaviourOnOptimizationLevelChanged;
    }

    private void OnDisable()
    {
        OptimizatonBehaviour.OptimizationLevelChanged -= OptimizatonBehaviourOnOptimizationLevelChanged;
    }

    private void OptimizatonBehaviourOnOptimizationLevelChanged(int obj)
    {
        QualitySettings.antiAliasing = 3 - obj;
    }
}