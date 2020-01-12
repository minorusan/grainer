using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;

public class LevelsHistory
{
    private static Level[] levels;
    private static LevelsDatabaseStructure serversideLevelsData;
    private static readonly string PLAYER_DATA_PATH = Application.persistentDataPath + "/player_data.json";
    private const string CURRENT_LEVEL_KEY = "SAskaldjslajk34654ghdyhjbnffdljaskdlfgsdfg";

    public static int CurrentLevelID
    {
        get => PlayerPrefs.GetInt(CURRENT_LEVEL_KEY);
        set => PlayerPrefs.SetInt(CURRENT_LEVEL_KEY, value);
    }

    public static int GamePlayLevelID;

    public static void RefreshLevelsDatabase(Action success, Action failure)
    {
        Networking.GetItems((delegate(DatabaseItem[] items)
        {
            serversideLevelsData = new LevelsDatabaseStructure {content = items};
            success?.Invoke();
            File.WriteAllText(PLAYER_DATA_PATH, JsonUtility.ToJson(serversideLevelsData));
#if UNITY_EDITOR
            File.WriteAllText(AssetDatabase.GetAssetPath(Resources.Load<TextAsset>("defaultlevelsdata")),
                JsonUtility.ToJson(serversideLevelsData));
#endif
        }), (string failureFromServer)=> { failure?.Invoke(); });
    }

    public static bool ComparePlayerLevelDataWithServer(int levelID, out float compareResult)
    {
        if (levelID > CurrentLevelID)
        {
            compareResult = 0f;
            Debug.Log("LevelsHistory::Will not compare to level not yet passed or opened");
            return false;
        }
        
        if (serversideLevelsData == null)
        {
            compareResult = 0f;
            return false;
        }

        var levelOnServer = serversideLevelsData.content.FirstOrDefault(x => x != null && x.levelID == levelID);
        if (levelOnServer == null)
        {
            compareResult = 0f;
            Debug.LogError("LevelsHistory::Some fuckup took place. No such level on server");
            return false;
        }

        var playerTurnsCount = TurnsCountForLevel(levelID);
        compareResult = (float)serversideLevelsData.content[levelID].minTurnsCount / playerTurnsCount;
        Debug.Log($"LevelsHistory::Comparing player level <{levelID}>(turns count <{playerTurnsCount}>) " +
                  $"with server level <{levelID}>(turns count <{serversideLevelsData.content[levelID].minTurnsCount}>). Result::{compareResult}");
        return true;
    }

    public static int TurnsCountForLevel(int levelID)
    {
        var data = LevelsDatabaseStructure();
        return data.content.First(x=>x.levelID == levelID).minTurnsCount;
    }

    public static Texture2D GetLevelMap()
    {
        if (levels == null)
        {
            levels = GetAndSortLevels();
        }
        
        return levels[GamePlayLevelID].levelTexture;
    }

    private static Level[] GetAndSortLevels()
    {
        var allLevels = Resources.LoadAll<Level>("Levels").ToList();

        return allLevels.OrderBy(x=>Convert.ToInt32(x.levelTexture.name)).ToArray();
    }

    public static bool PassLevel(int levelID, int turnsCount = 100)
    {
        if (levelID >= CurrentLevelID)
        {
            CurrentLevelID = levelID + 1;
        }
        
        return UpdatePlayerData(levelID, turnsCount);
    }

    private static bool UpdatePlayerData(int levelID, int turnsCount = 100)
    {
        var data = LevelsDatabaseStructure();

        var databaseItems = data.content.ToList();
        var levelItem = databaseItems.FirstOrDefault(x => x!=null && x.levelID == levelID);
        if (levelItem == null)
        {
            databaseItems.Add(new DatabaseItem(){levelID = levelID, minTurnsCount = turnsCount});
        }
        else
        {
            levelItem.minTurnsCount = turnsCount;
            databaseItems[databaseItems.IndexOf(levelItem)] = levelItem;
        }

        data.content = databaseItems.ToArray();
        File.WriteAllText(PLAYER_DATA_PATH, JsonUtility.ToJson(data));
        Debug.Log($"LevelHistory::Progress saved to {PLAYER_DATA_PATH}");
        return UpdateRemoteLevelResultIfNeeded(levelID, turnsCount);
    }

    public static bool UpdateRemoteLevelResultIfNeeded(int levelID, int turnsCount)
    {
        if (ComparePlayerLevelDataWithServer(levelID, out var compareResult) && compareResult > 1f)
        {
            RefreshLevelsDatabase(() =>
            {
                Networking.UpdateItem(levelID, turnsCount, null, null);
            }, () =>
            {
               //TODO::Save player success for future 
            });
            return true;
        }

        return false;
    }

    private static LevelsDatabaseStructure LevelsDatabaseStructure()
    {
        var data = new LevelsDatabaseStructure();
        if (File.Exists(PLAYER_DATA_PATH))
        {
            data = JsonUtility.FromJson<LevelsDatabaseStructure>(File.ReadAllText(PLAYER_DATA_PATH));
        }
        else
        {
            data = JsonUtility.FromJson<LevelsDatabaseStructure>(Resources.Load<TextAsset>("defaultlevelsdata")
                .text);
        }

        return data;
    }

#if UNITY_EDITOR
    [MenuItem("Grainer/Clear local data")]
#endif
    public static void ClearHistory()
    {
        LevelsHistory.CurrentLevelID = 0;
        File.Delete(PLAYER_DATA_PATH);
    }
}
