using UnityEngine;
using UnityEngine.UI;

public class ObjectiveFillBehaviour : MonoBehaviour
{
    public Image TrackingImage;
    // Start is called before the first frame update
    void Awake()
    {
        GameplayObjectivesBehaviour.ObjectiveStateChanged += GameplayObjectivesBehaviourOnObjectiveStateChanged;
    }

    private void GameplayObjectivesBehaviourOnObjectiveStateChanged(float obj)
    {
        TrackingImage.fillAmount = obj;
    }

    private void OnDisable()
    {
        GameplayObjectivesBehaviour.ObjectiveStateChanged -= GameplayObjectivesBehaviourOnObjectiveStateChanged;
    }
}