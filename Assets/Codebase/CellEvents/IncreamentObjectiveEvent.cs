using UnityEngine;

[CreateAssetMenu(fileName = "New increament objective event", menuName = "Grainer/Events/Increament objective")]
public class IncreamentObjectiveEvent : EventDefinition
{
    protected override void InvokeEvent(GameObject cell)
    {
        GameplayObjectivesBehaviour.Increament();
    }
}
