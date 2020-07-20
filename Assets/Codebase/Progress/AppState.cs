﻿using UnityEngine;

public static class AppState 
{
    private const string CURRENT_LEVEL_KEY = "SAskaldjslajk34654ghdyhjbnffdljaskdlfgsdfg";

    public static int LastOpenedLevelNumber
    {
        get
        {
            var lastOne = PlayerPrefs.GetInt(CURRENT_LEVEL_KEY);
            if (lastOne <= 0)
            {
                LastOpenedLevelNumber = 1;
            }

            return lastOne;
        } 
        private set => PlayerPrefs.SetInt(CURRENT_LEVEL_KEY, value);
    }

    public static void PassLevelIfNeeded()
    {
        if (GameplayLevelNumber >= LastOpenedLevelNumber)
        {
            LastOpenedLevelNumber++;
        }
    }

    public static void PassAll()
    {
        LastOpenedLevelNumber = 900;
    }

    public static void ClearProgress()
    {
        LastOpenedLevelNumber = 1;
    }

    public static int GameplayLevelNumber;
}
