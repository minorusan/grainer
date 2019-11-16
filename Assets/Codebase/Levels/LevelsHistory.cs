using System;
using System.IO;
using System.Linq;
using UnityEngine;

public class LevelsHistory
{
    private static Level[] levels;
    private static LevelsDatabaseStructure serversideLevelsData;
    private static readonly string PLAYER_DATA_PATH = Application.persistentDataPath + "/player_data.json";
    private const string CURRENT_LEVEL_KEY = "SAskaldjslajk34654ghdyhjbnffdljaskdlfgsdfg";

    public static int CurrentLevelID
    {
        get => PlayerPrefs.GetInt(CURRENT_LEVEL_KEY) + 1;
        set
        {
            PlayerPrefs.SetInt(CURRENT_LEVEL_KEY, value);
        }
    }

    public static int GamePlayLevelID;

    public static void RefreshLevelsDatabase(Action success, Action failure)
    {
        Networking.GetItems((delegate(DatabaseItem[] items)
        {
            serversideLevelsData = new LevelsDatabaseStructure {content = items};
            success?.Invoke();
        }), (string failureFromServer)=> { failure?.Invoke(); });
    }

    public static bool ComparePlayerLevelDataWithServer(int levelID, out float compareResult)
    {
        if (levelID > CurrentLevelID - 1)
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
            levels = Resources.LoadAll<Level>("Levels");
        }

        return levels[GamePlayLevelID - 1].levelTexture;
    }

    public static void PassLevel(int levelID, int turnsCount = 100)
    {
        if (levelID >= CurrentLevelID)
        {
            CurrentLevelID = levelID;
        }
        
        UpdatePlayerData(levelID, turnsCount);
    }

    private static void UpdatePlayerData(int levelID, int turnsCount = 100)
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
        UpdateRemoteLevelResultIfNeeded(levelID, turnsCount);
    }

    public static void UpdateRemoteLevelResultIfNeeded(int levelID, int turnsCount)
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
        }
    }

    private static LevelsDatabaseStructure LevelsDatabaseStructure()
    {
        var data = new LevelsDatabaseStructure();
        if (File.Exists(PLAYER_DATA_PATH))
        {
            data = JsonUtility.FromJson<LevelsDatabaseStructure>(File.ReadAllText(PLAYER_DATA_PATH));
        }

        return data;
    }
}
