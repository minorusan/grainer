using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Events;

public class AdvertisementBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    private const string PLAYED_LEVELS_KEY = "cygh hfh78 tyr67w3gtfr7gbvsjfds";
    public UnityEvent OnAdCompleted;

    void Start()
    {
        if (!Advertisement.isInitialized)
        {
            AdsManager.Init();
        }
        AdsManager.Load();
    }

    public void ShowAd()
    {
        var playedLevels = PlayerPrefs.GetInt(PLAYED_LEVELS_KEY);
        if (playedLevels >= AdsManager.LEVELS_BEFORE_AD && AppState.LastOpenedLevelNumber > AdsManager.MINIMUM_ADS_LEVEL)
        {
            PlayerPrefs.SetInt(PLAYED_LEVELS_KEY, 0); 
            AdsManager.ShowAd(OnAdCompleted.Invoke);
        }
        else
        {
            PlayerPrefs.SetInt(PLAYED_LEVELS_KEY, playedLevels + 1); 
            OnAdCompleted.Invoke();
        }
    }
}
