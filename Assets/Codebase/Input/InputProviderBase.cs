using System;
using UnityEngine;

public abstract class InputProviderBase : MonoBehaviour
{
    public static event Action<InputChangedEventArgs> InputChanged = delegate(InputChangedEventArgs arg) { };

    protected void InvokeEvent(MovementDirection direction)
    {
        InputChanged(new InputChangedEventArgs(direction, Time.timeSinceLevelLoad));
    }
}