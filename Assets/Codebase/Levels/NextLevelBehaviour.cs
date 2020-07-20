using UnityEngine;

public class NextLevelBehaviour : MonoBehaviour
{
    public void Continue()
    {
        AppState.GameplayLevelNumber += 1;
    }
}