using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CellComponentHolderBehaviour : MonoBehaviour
{
    public CellComponentType Type;
    public UnityEvent Activate;

    public void ActivateComponent()
    {
        Activate.Invoke();
    }
}
