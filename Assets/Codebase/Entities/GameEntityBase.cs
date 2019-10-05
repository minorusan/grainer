using UnityEngine;

public class GameEntityBase : MonoBehaviour
{
    public GameEntityType Type;

    private void OnEnable()
    {
        GameEntitiesHolder.RegisterEntity(this);
    }

    private void OnDisable()
    {
        GameEntitiesHolder.UnregisterEntity(this);
    }
}
