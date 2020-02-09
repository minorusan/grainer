using UnityEngine;

public class InputListenerBehaviour : MonoBehaviour
{
    public MovementBehaviour MovementBehaviour;

    void Start()
    {
        InputProviderBase.InputChanged += OnInputChanged;
    }

    private void OnDisable()
    {
        InputProviderBase.InputChanged -= OnInputChanged;
    }

    private void OnInputChanged(InputChangedEventArgs obj)
    {
        if (GameplayTimescale.GameActive && !TutorialBehaviour.TutorialCompleted)
        {
            TutorialBehaviour.TutorialCompleted = true;
        }
        MovementBehaviour.SetDirection(obj.Direction);
    }
}