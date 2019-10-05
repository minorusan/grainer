using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "New log event", menuName = "Grainer/Events/Log")]
public class LogCellEvent : EventDefinition
{
    protected override void InvokeEvent(GameObject cell)
    {
        var result = new StringBuilder();
        result.Append($"Called {Type} on cell {cell} at position {cell.transform.position}");
    }
}