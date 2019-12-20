﻿using DG.Tweening;
using UnityEngine;

public class CameraFieldOfViewBehaviour : MonoBehaviour
{
    public float MaxFieldOfView;
    public float MinFieldOfView;
    public Vector2 MaxTextureSize;

    public void DoFieldOfView(Vector2 textureSize)
    {
        
        var textureRatio = textureSize.magnitude / MaxTextureSize.magnitude;
        var fieldOfView = Mathf.Lerp(MinFieldOfView, MaxFieldOfView, textureRatio);
        if (textureSize.y >= textureSize.x)
        {
            fieldOfView *= 1.4f;
        }
        Camera.main.DOFieldOfView(fieldOfView, 1f);
    }
}
