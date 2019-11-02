using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New level finished event", menuName = "Grainer/Events/Level complete")]
public class LevelFinishedEvent : EventDefinition
{
    protected override void InvokeEvent(GameObject cell)
    {
        GameFinishedBroadcastBehaviour.InvokeLevelFinished();
    }
}
