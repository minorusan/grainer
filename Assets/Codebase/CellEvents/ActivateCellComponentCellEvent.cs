using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "New activation event", menuName = "Grainer/Events/Cell component activation")]
public class ActivateCellComponentCellEvent : EventDefinition
{
    public CellComponentType ComponentType;

    protected override void InvokeEvent(GameObject cell)
    {
        var components = cell.GetComponentsInChildren<CellComponentHolderBehaviour>();
        components.Where(x=>x.Type == ComponentType).ToList().ForEach(x=>x.ActivateComponent());
    }
}