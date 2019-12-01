using UnityEngine;

public class RandomYRotationComponent : MonoBehaviour
{
    public float RotationMin = 0f;
    public float RotationMax = 360f;
    
    private void OnEnable()
    {
        var euler = transform.eulerAngles;
        euler.y = Random.Range(RotationMin, RotationMax);
        transform.eulerAngles = euler;
    }
}