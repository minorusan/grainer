using Crysberry.Routines;
using UnityEngine;

public abstract class EventDefinition : ScriptableObject
{
    public CellEventType Type;
    public float Delay;

    public void Invoke(GameObject cell)
    {
        Routiner.InvokeDelayed(() => { InvokeEvent(cell); }, Delay);
    }

    protected abstract void InvokeEvent(GameObject cell);
}