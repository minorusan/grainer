using UnityEngine;

public class NextLevelBehaviour : MonoBehaviour
{
    public void Continue()
    {
        LevelsHistory.GamePlayLevelID += 1;
    }
}