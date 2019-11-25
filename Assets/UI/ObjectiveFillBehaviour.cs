using System;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveFillBehaviour : MonoBehaviour
{
    public Image TrackingImage;

    private float currentFillAmount;

    // Start is called before the first frame update
    void Awake()
    {
        currentFillAmount = 0;
        TrackingImage.fillAmount = 0;
        
        GameplayObjectivesBehaviour.ObjectiveStateChanged += GameplayObjectivesBehaviourOnObjectiveStateChanged;
    }

    private void GameplayObjectivesBehaviourOnObjectiveStateChanged(float obj)
    {
        currentFillAmount = obj;
    }

    private void FixedUpdate()
    {
        if (TrackingImage.fillAmount < currentFillAmount)
        {
            TrackingImage.fillAmount = Mathf.Lerp(TrackingImage.fillAmount, currentFillAmount, Time.deltaTime);
        }
    }

    private void OnDisable()
    {
        GameplayObjectivesBehaviour.ObjectiveStateChanged -= GameplayObjectivesBehaviourOnObjectiveStateChanged;
    }
}