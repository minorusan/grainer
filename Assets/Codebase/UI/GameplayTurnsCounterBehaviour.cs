using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameplayTurnsCounterBehaviour : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMesh;
    private int currentTurnsCount;
    private void Awake()
    {
        currentTurnsCount = 0;
        GameplayTurnsCountBehaviour.PlayerTurnsCountChanged += IncreaseTurnCount;
        textMesh.text = currentTurnsCount.ToString();
    }

    private void IncreaseTurnCount(int count)
    {
        currentTurnsCount = count;
        textMesh.text = currentTurnsCount.ToString();
    }

    private void OnDisable()
    {
        GameplayTurnsCountBehaviour.PlayerTurnsCountChanged -= IncreaseTurnCount;
    }

    public int GetCurrentTurnsCount()
    {
        return currentTurnsCount;
    }
}
