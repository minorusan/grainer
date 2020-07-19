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
        LevelIndexText.text = isActive ? (levelIndex + 1).ToString() : string.Empty;
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
