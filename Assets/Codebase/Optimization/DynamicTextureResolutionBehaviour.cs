using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicTextureResolutionBehaviour : MonoBehaviour
{
    public Material MainMaterial;
    public Texture2D[] QualityTextures;
    
    private void Awake()
    {
        OptimizatonBehaviour.OptimizationLevelChanged += OptimizatonBehaviourOnOptimizationLevelChanged;
    }

    private void OnDisable()
    {
        OptimizatonBehaviour.OptimizationLevelChanged -= OptimizatonBehaviourOnOptimizationLevelChanged;
    }

    private void OptimizatonBehaviourOnOptimizationLevelChanged(int obj)
    {
        MainMaterial.mainTexture = QualityTextures[Mathf.Clamp(obj, 0, QualityTextures.Length -1)];
    }
}