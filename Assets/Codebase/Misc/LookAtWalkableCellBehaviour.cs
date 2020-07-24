using UnityEngine;

public class LookAtWalkableCellBehaviour : MonoBehaviour
{
    void Start()
    {
        transform.LookAt(AreaHelper.NearestWalkablePosition(transform.position));
    }

    public void Look()
    {
        Start();
    }
}