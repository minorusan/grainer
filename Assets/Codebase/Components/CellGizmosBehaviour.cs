using UnityEngine;

public class CellGizmosBehaviour : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        for (int i = -2; i <= 2; i++)
        {
            for (int j = -2; j <= 2; j++)
            {
                var position = transform.position + new Vector3(i, 0f, j);
                var walkable = AreaHelper.IsWalkable(position);
                Gizmos.color = walkable ? Color.green : Color.red;
                Gizmos.DrawWireCube(position, Vector3.one);
            }
        }
    }
}
