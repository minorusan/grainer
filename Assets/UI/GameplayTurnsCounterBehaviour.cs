﻿using DG.Tweening;
using TMPro;
using UnityEngine;

public class GameplayTurnsCounterBehaviour : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMesh;
    public GameObject Container;
    public float ShakeTime, ShakeMultiplier;
    private int currentTurnsCount;
    private void Awake()
    {
        currentTurnsCount = 0;
        GameplayTurnsCountBehaviour.PlayerTurnsCountChanged += IncreaseTurnCount;
        textMesh.text = currentTurnsCount.ToString();
    }

    private void IncreaseTurnCount(int count)
    {
        Container.transform.DOKill(true);
        Container.transform.rotation = Quaternion.identity;
        Container.transform.DOShakeRotation(ShakeTime, Vector3.one * ShakeMultiplier);
        currentTurnsCount = count;
        textMesh.text = currentTurnsCount >= 0 ? currentTurnsCount.ToString() : "x_x";
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