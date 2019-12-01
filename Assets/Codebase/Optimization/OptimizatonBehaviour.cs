using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Crysberry.Routines;
using UnityEngine;

public class OptimizatonBehaviour : MonoBehaviour
{
    private const int MAX_OPTIMIZATION = 3;
    private const string OPTIMISATION_LEVEL_DETECTED_KEY = "sjkglyuy47346tygtfyuigshfjghjsd";
    private const string OPTIMISATION_LEVEL_KEY = "shadjkhfkjheiufgydshugbfajsdf";
    private const string AVERAGE_FRAMERATE_KEY = "JDKDLSFJKLsdifoua8eirujioghsiufogjsdrfg";
    private List<float> deltaTimes;
    private float currentTime;
    private bool dataAchieved;
    private bool optimizationCheck;
    public int TargetFPS = 60;
    public int TestFramesAmount = 300;
    public static event Action<int> OptimizationLevelChanged = delegate(int i) {  }; 

    public static int OptimizationLevel => PlayerPrefs.GetInt(OPTIMISATION_LEVEL_KEY);
    public static float AverageFrameRate => PlayerPrefs.GetFloat(AVERAGE_FRAMERATE_KEY);
    public static bool OptimizationLevelGoalReached => PlayerPrefs.GetInt(OPTIMISATION_LEVEL_DETECTED_KEY) > 5;

    private void Awake()
    {
        Application.targetFrameRate = TargetFPS;
    }

    public static void ResetOptimizationSettings()
    {
        var optimizer = FindObjectOfType<OptimizatonBehaviour>();
        optimizer.enabled = false;
        Routiner.InvokeDelayed(() => { optimizer.enabled = true; }, 1f);
    }

    private void OnEnable()
    {
        Routiner.InvokeDelayed(InitializeCheck, 1f);
    }

    private void InitializeCheck()
    {
        if (!OptimizationLevelGoalReached)
        {
            currentTime = 0f;
            optimizationCheck = true;
            deltaTimes = new List<float>(TestFramesAmount);
            Debug.Log($"Optimization checker::Starting performance check");
        }
        else
        {
            Debug.Log($"Optimization checker::Setting optimization level to {OptimizationLevel}");
            Routiner.InvokeNextFrame(() =>
            {
                OptimizationLevelChanged(OptimizationLevel);
            });
            
            Routiner.InvokeDelayed((()=> { Resources.UnloadUnusedAssets(); }), 1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (optimizationCheck && deltaTimes.Count <= TestFramesAmount)
        {
            deltaTimes.Add(Time.deltaTime);
        }
        else
        {
            if (deltaTimes == null || deltaTimes.Count <= 0) return;
            optimizationCheck = false;
            CheckOptimization();
        }
    }

    private void CheckOptimization()
    {
        var avarageFrameLength = 1.0f/deltaTimes.Average();
        Debug.Log($"Optimization checker:: Average FPS is {avarageFrameLength}");
        if ((avarageFrameLength / (float)TargetFPS) < 0.8f && !dataAchieved)
        {
            var level = PlayerPrefs.GetInt(OPTIMISATION_LEVEL_KEY);
            level = Mathf.Clamp(level + 1, 0, MAX_OPTIMIZATION);
            PlayerPrefs.SetInt(OPTIMISATION_LEVEL_KEY, level);
            OptimizationLevelChanged(level);
           
            Debug.Log($"Optimization checker::Changed optimization level to {level}. Average frameRate:{avarageFrameLength}");
            if (level == MAX_OPTIMIZATION)
            {
                PlayerPrefs.SetInt(OPTIMISATION_LEVEL_DETECTED_KEY, 1);
                PlayerPrefs.SetFloat(AVERAGE_FRAMERATE_KEY, avarageFrameLength);
                Routiner.InvokeDelayed(ResetOptimizationSettings, 1f);
                Debug.Log($"Optimization checker::Max optimization level reached");
            }
            else
            {
                Debug.Log($"Optimization checker::Scheduled FPS check..");
                Routiner.InvokeDelayed(() =>
                {
                    Resources.UnloadUnusedAssets();
                }, 1f);
                Routiner.InvokeDelayed(OnEnable, 4f);
            }
        }
        else
        {
            PlayerPrefs.SetInt(OPTIMISATION_LEVEL_DETECTED_KEY, 100);
            
            if (!dataAchieved)
            {
                Routiner.InvokeDelayed(ResetOptimizationSettings, 1f);
                dataAchieved = true;
            }
            else
            {
                PlayerPrefs.SetFloat(AVERAGE_FRAMERATE_KEY, avarageFrameLength);
                Debug.Log($"Optimization checker::Required performance achieved. Optimization level is {OptimizationLevel}");
            }
        }
        deltaTimes.Clear();
    }
}