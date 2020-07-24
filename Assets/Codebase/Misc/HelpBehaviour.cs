using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HelpBehaviour : MonoBehaviour
{
    public UnityEvent OnAdShown;
    
    public void ShowHelp()
    {
        AdsManager.ShowAd(OnAdShown.Invoke);
    }
}
