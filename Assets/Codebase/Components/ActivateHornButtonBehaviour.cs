using Crysberry.Routines;
using UnityEngine;

public class ActivateHornButtonBehaviour : MonoBehaviour
{
    public GameObject Horn;
    void Start()
    {
        Routiner.InvokeNextFrame(() =>
        {
            if (FindObjectOfType<AnimalComponent>() != null)
            {
                Horn.gameObject.SetActive(true);
            }
        });
    }
}