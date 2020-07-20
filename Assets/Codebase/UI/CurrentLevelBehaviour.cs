using TMPro;
using UnityEngine;

public class CurrentLevelBehaviour : MonoBehaviour
{
    public TextMeshProUGUI CurrentLevel;

    private void Start()
    {
        CurrentLevel.text = (AppState.GameplayLevelNumber).ToString();
    }
}