using UnityEngine;

public class RandomYRotationComponent : MonoBehaviour
{
    private void OnEnable()
    {
        var rotations = new[] { 0, 90f, 180f, 270f, 360f};
        var euler = transform.eulerAngles;
        euler.y = rotations[Random.Range(0, rotations.Length)];
        transform.eulerAngles = euler;
    }
}