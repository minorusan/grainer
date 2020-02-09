using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FrameDataBehaviour : MonoBehaviour
{
    private static FrameDataBehaviour instance;

    public static float FreshFrameData => instance == null ? 0f : 1.0f / instance.frameData.Average();
    private List<float> frameData;
    public int SamplesCount;
    private void Awake()
    {
        instance = this;
        frameData = new List<float>(SamplesCount);
        OptimizatonBehaviour.OptimizationLevelChanged += OptimizatonBehaviourOnOptimizationLevelChanged;
    }

    private void OptimizatonBehaviourOnOptimizationLevelChanged(int obj)
    {
        frameData.Clear();
    }

    private void OnDisable()
    {
        instance = null;
        OptimizatonBehaviour.OptimizationLevelChanged -= OptimizatonBehaviourOnOptimizationLevelChanged;
    }

    private void Update()
    {
        if (frameData != null)
        {
            frameData.Add(Time.deltaTime);
            if (frameData.Count >= SamplesCount)
            {
                frameData.Remove(frameData[0]);
            }
        }
    }
}
