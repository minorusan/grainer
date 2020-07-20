using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelPreviewBehaviour : MonoBehaviour
{
    public TextMeshProUGUI LevelIndexText;
    public Button LoadButton;

    public void Init(int levelIndex)
    {
        var isActive = levelIndex <= AppState.LastOpenedLevelNumber;
        LevelIndexText.text = isActive ? (levelIndex).ToString() : string.Empty;
        LoadButton.interactable = isActive;

        if (isActive)
        {
            LoadButton.onClick.AddListener(() =>
            {
                AppState.GameplayLevelNumber = levelIndex;
                FindObjectOfType<LoadSceneComponent>().LoadScene("gameplay");
            });
        }
    }
}
