using UnityEngine;

public class DontDestroyOnLoadComponent : MonoBehaviour
{
    public string id;
    void Start()
    {
        var instances = FindObjectsOfType<DontDestroyOnLoadComponent>();
        foreach (var component in instances)
        {
            if (component.id == id && component.gameObject != gameObject)
            {
                Destroy(component.gameObject);
            }
        }
        DontDestroyOnLoad(gameObject);
    }
}