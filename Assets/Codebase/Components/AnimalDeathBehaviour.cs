using UnityEngine;

public class AnimalDeathBehaviour : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        GameplayTimescale.GameActive = false;
        FindObjectOfType<GameOutcomeBehaviour>().ForceLoose();
    }
}
