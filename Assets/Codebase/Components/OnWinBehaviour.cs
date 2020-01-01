using UnityEngine;
using UnityEngine.Events;

public class OnWinBehaviour : MonoBehaviour
{
    public UnityEvent OnObjectivesGathered;
    void Start()
    {
        GameplayObjectivesBehaviour.ObjectiveStateChanged += GameplayObjectivesBehaviourOnObjectiveStateChanged;
    }

    private void GameplayObjectivesBehaviourOnObjectiveStateChanged(float obj)
    {
        if (GameplayObjectivesBehaviour.IsCompleted)
        {
            OnObjectivesGathered.Invoke();
        }
    }

    private void OnDisable()
    {
        GameplayObjectivesBehaviour.ObjectiveStateChanged -= GameplayObjectivesBehaviourOnObjectiveStateChanged;
    }
}
