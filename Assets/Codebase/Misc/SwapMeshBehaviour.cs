using UnityEngine;

public class SwapMeshBehaviour : MonoBehaviour
{
    public Material False;
    public Material True;
    
    public void Swap(bool value)
    {
        GetComponent<MeshRenderer>().material = value ? True : False;
    }
}