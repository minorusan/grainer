using UnityEngine;

public class DontDestroyOnLoadComponent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}