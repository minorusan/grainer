using UnityEngine;

public class ReferencingGizmoComponent : MonoBehaviour
{
    public bool Extended;

    private void OnDrawGizmos()
    {
        if (Extended)
        {
            
            for (int i = -2; i <= 2; i++)
            {
                for (int j = -2; j <= 2; j++)
                {
                    if (i == 0 && j == 0)
                    {
                        continue;
                    }
                    Gizmos.color = Color.grey;
                    if (i > 1 || i < -1 || j > 1 || j < -1)
                    {
                        Gizmos.color = Color.red;;
                    }
                    Gizmos.DrawCube(new Vector3(i, 0f, j), Vector3.one);
                }
            }
        }
        Gizmos.color = Color.green;
        Gizmos.DrawCube(Vector3.zero, Vector3.one);
    }
}