using System;
using UnityEngine;

public abstract class InputProviderBase : MonoBehaviour
{
    public bool IsEnabled = true;
    public static event Action<InputChangedEventArgs> InputChanged = delegate(InputChangedEventArgs arg) { };

    protected void InvokeEvent(MovementDirection direction)
    {
        if (IsEnabled)
        {
            InputChanged(new InputChangedEventArgs(direction, Time.timeSinceLevelLoad));
        }
    }
}