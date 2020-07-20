using System;
using System.IO;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor; 
#endif
using UnityEngine;

public class LevelsStorage
{
    private const string DEFAULT_LEVELS_DATA_NAME = "defaultlevelsdata";
    private static readonly string PLAYER_DATA_PATH = Application.persistentDataPath + "/player_data.json";
    private static LevelsDatabaseStructure currentData;
    private static Level[] inAppLevels;

    public static void Update()
    {
        GetLocalLevelsIfNeeded();
        GetDatabaseFromRemote(GetLocalDatabase);
    }

    public static int LastLevelNumber => inAppLevels.Length + 1;

    public static Texture2D LevelMapForLevelNumber(int levelNumber)
    {
        var level = inAppLevels.FirstOrDefault(x => x.Number == levelNumber);
        if (level == null)
        {
            throw new ArgumentException($"No level with number {levelNumber}");
        }

        return level.levelTexture;
    }

    public static int TurnsCountForLevelNumber(int levelNumber)
    {
        return TurnsCountForLevelID(LevelIDForLevelNumber(levelNumber));
    }
    
    public static int TurnsCountForLevelID(int levelID)
    {
        var version = LevelVersionForLevelID(levelID);
        var level = currentData.content.FirstOrDefault(x => x.levelID == levelID && x.version == version);
        if (level == null)
        {
            return 100;
        }
        return level.minTurnsCount;
    }

    public static int LevelIDForLevelNumber(int levelNumber)
    {
        var level = inAppLevels.FirstOrDefault(x => x.Number == levelNumber);
        if (level == null)
        {
            throw new ArgumentException($"No level with number {levelNumber}");
        }

        return level.Id;
    }

    public static int LevelVersionForLevelID(int levelID)
    {
        var level = inAppLevels.FirstOrDefault(x => x.Id == levelID);
        if (level == null)
        {
            throw new ArgumentException($"No level with level ID {levelID}");
        }
        return level.version;
    }

    public static bool IsPlayerResultBetterThenRemote(int levelNumber, int playerResult)
    {
        var turnsCount = TurnsCountForLevelID(LevelIDForLevelNumber(levelNumber));
        return playerResult < turnsCount;
    }

    public static void UpdateLevel(int levelNumber, int playerTurnsCount)
    {
        var levelID = LevelIDForLevelNumber(levelNumber);
        var version = LevelVersionForLevelID(levelID);
        var level = currentData.content.FirstOrDefault(x => x.levelID == levelID && x.version == version);
        if (level != null)
        {
            level.minTurnsCount = playerTurnsCount;
        }
        else
        {
            var contentAsList = currentData.content.ToList();
            contentAsList.Add(new DatabaseItem()
            {
                levelID = levelID,
                version = version,
                minTurnsCount = playerTurnsCount
            });
            currentData.content = contentAsList.ToArray();
        }
        
        Networking.UpdateItem(levelID, version, playerTurnsCount, null, null);
    }
    
    private static void GetLocalDatabase()
    {
        currentData = new LevelsDatabaseStructure();
        if (File.Exists(PLAYER_DATA_PATH))
        {
            currentData = JsonUtility.FromJson<LevelsDatabaseStructure>(File.ReadAllText(PLAYER_DATA_PATH));
        }
        else
        {
            currentData = JsonUtility.FromJson<LevelsDatabaseStructure>(Resources.Load<TextAsset>(DEFAULT_LEVELS_DATA_NAME)
                .text);
        }
    }

    private static void GetLocalLevelsIfNeeded()
    {
        if (inAppLevels == null)
        {
            inAppLevels = Resources.LoadAll<Level>("Levels");
        }
    }

    private static void SaveLocalDatabase()
    {
        File.WriteAllText(PLAYER_DATA_PATH, JsonUtility.ToJson(currentData));
#if UNITY_EDITOR
        File.WriteAllText(AssetDatabase.GetAssetPath(Resources.Load<TextAsset>(DEFAULT_LEVELS_DATA_NAME)),
            JsonUtility.ToJson(currentData));
#endif
    }
    
    private static void GetDatabaseFromRemote(Action failure)
    {
        Networking.GetItems((delegate(DatabaseItem[] items)
        {
            currentData = new LevelsDatabaseStructure {content = items};
            SaveLocalDatabase();
        }), (string failureFromServer) =>
        {
            failure?.Invoke();
        });
    }
}