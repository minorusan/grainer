using UnityEngine;

[CreateAssetMenu(fileName = "New walkable status event", menuName = "Grainer/Events/Walkable status toggle")]
public class ToggleWalkableStatusCellEvent : EventDefinition
{
    public bool IsWalkable;

    protected override void InvokeEvent(GameObject cell)
    {
        AreaHelper.SetWalkable(cell.transform.position, IsWalkable);
    }
}