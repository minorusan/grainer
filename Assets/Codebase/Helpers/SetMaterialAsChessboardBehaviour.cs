using UnityEngine;

public class SetMaterialAsChessboardBehaviour : MonoBehaviour
{
    [SerializeField] private Material[] materials;
    [SerializeField] private MeshRenderer[] meshes;

    public void SetMaterial(int i, int j)
    {
        var materialNumber = GetMaterialNumber(i, j);

        foreach (var meshRenderer in meshes)
        {
            meshRenderer.material = materials[materialNumber];
        }
    }

    private int GetMaterialNumber(int i, int j)
    {
        return i % 2 == 0 ? j % 2 == 0 ? 0 : 1 : j % 2 == 0 ? 1 : 0;
    }
}