using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DebuggableBehaviour : MonoBehaviour
{
    public bool DebugMode;

    protected virtual void Awake()
    {
#if VERBOSE_DEBUG
        DebugMode = true;
#endif
    }

    private void OnDrawGizmos()
    {
        if (DebugMode)
        {
            GizmosDebug();
        }
    }

    protected abstract void GizmosDebug();
}
