using UnityEngine;

public class AnimalDeathBehaviour : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        FindObjectOfType<GameOutcomeBehaviour>().ForceLoose();
    }
}
