using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ToggleGameTimeBehaviour : MonoBehaviour
{
    public bool IsActive;
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => { GameplayTimescale.GameActive = IsActive; });
    }
}