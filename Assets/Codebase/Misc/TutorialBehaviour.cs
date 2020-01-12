using System;
using UnityEngine;

public class TutorialBehaviour : MonoBehaviour
{
    public static bool TutorialCompleted
    {
        get
        {
            return PlayerPrefs.HasKey("tutorial_shown");
        }
        set
        {
            PlayerPrefs.SetInt("tutorial_shown", 1);
            FindObjectOfType<TutorialBehaviour>().gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        if (TutorialCompleted)
        {
            gameObject.SetActive(false);
        }
    }
}