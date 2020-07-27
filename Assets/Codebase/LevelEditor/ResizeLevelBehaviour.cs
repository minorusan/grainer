using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResizeLevelBehaviour : MonoBehaviour
{
    public InputField width;
    public InputField height;

    private void OnEnable()
    {
        var levelEditor = FindObjectOfType<LevelEditor.LevelEditor>();
        width.text = levelEditor.CurrentLevel.levelTexture.width.ToString();
        height.text = levelEditor.CurrentLevel.levelTexture.height.ToString();
    }
}
