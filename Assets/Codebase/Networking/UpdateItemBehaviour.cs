using UnityEngine;

public class UpdateItemBehaviour : MonoBehaviour
{
    public int LevelID;
    public int MinTurnsCount;

    private void OnEnable()
    {
        Networking.UpdateItem(LevelID, MinTurnsCount, s => Debug.Log(s), s => Debug.LogError(s));
    }
}
