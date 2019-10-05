using UnityEngine;

public class InputInitializeBehaviour : MonoBehaviour
{
    private void Start()
    {
        Instantiate(ResourceHelper.GetProvider(), transform);
    }
}