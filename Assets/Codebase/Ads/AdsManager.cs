using System;
using System.Collections.Generic;
using UnityEngine.Advertisements;

public class AdsManager : IUnityAdsListener
{
#if UNITY_IOS
    const string GAME_ID = "3380924";
#else
    const string GAME_ID = "3380925";
#endif
    private const string PLACEMENT_ID = "LevelComplete";
    public const int MINIMUM_ADS_LEVEL = 5;
    public const int LEVELS_BEFORE_AD = 10;
    
    
#if UNITY_EDITOR
    const bool TEST_MODE = true;
#else
    const bool TEST_MODE = false;
#endif
    
    private static AdsManager instance;
    private static Action currentCallback;
    private AdsManager()
    {
        
    }
    
    public static void Init()
    {    
        Advertisement.Initialize(GAME_ID, TEST_MODE);
        instance = new AdsManager();
        Advertisement.AddListener(instance);
    }

    public static void Load()
    {
        Advertisement.Load(PLACEMENT_ID);
    }

    public static void ShowAd(Action completion)
    {
        Amplitude.Instance.logEvent("ads_shown");
        Advertisement.Show();
        currentCallback = completion;
    }
    
    public void OnUnityAdsReady (string placementId) {
        // If the ready Placement is rewarded, activate the button: 
        if (placementId == PLACEMENT_ID) {        
            
        }
    }

    public void OnUnityAdsDidFinish (string placementId, ShowResult showResult) {
        // Define conditional logic for each ad completion status:
        currentCallback();
    }

    public void OnUnityAdsDidError (string message) {
        
    }

    public void OnUnityAdsDidStart (string placementId) {
        
    } 
}