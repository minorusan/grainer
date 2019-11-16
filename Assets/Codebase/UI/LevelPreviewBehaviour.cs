using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelPreviewBehaviour : MonoBehaviour
{
    public TextMeshProUGUI LevelIndexText;
    public Button LoadButton;

    public void Init(int levelIndex)
    {
        var isActive = levelIndex <= LevelsHistory.CurrentLevelID;
        LevelIndexText.text = isActive ? levelIndex.ToString() : "";
        LoadButton.interactable = isActive;

        if (isActive)
        {
            LoadButton.onClick.AddListener(() =>
            {
                LevelsHistory.GamePlayLevelID = levelIndex;
                FindObjectOfType<LoadSceneComponent>().LoadScene("gameplay");
            });
        }
    }
}
