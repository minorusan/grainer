using UnityEngine;

[CreateAssetMenu(fileName = "New spawn event", menuName = "Grainer/Events/Object spawn")]
public class SpawnGameObjectCellEvent : EventDefinition
{
    public GameObject[] Prefabs;
    [Range(0f, 1f)]
    public float RandomChance = 1f;
    public Vector3 Offset;

    protected override void InvokeEvent(GameObject cell)
    {
        if (Random.value <= RandomChance)
        {
            var instance = Instantiate(Prefabs[Random.Range(0, Prefabs.Length)]);
            instance.transform.position = cell.transform.position + Offset;
        }
    }
}