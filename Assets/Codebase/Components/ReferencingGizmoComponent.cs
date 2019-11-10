using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferencingGizmoComponent : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(Vector3.zero, Vector3.one);
    }
}
