using System.IO;
using System.Linq;
using UnityEngine;

public class LevelsHistory
{
    private static Level[] levels;
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
        var data = new GetItemsResponseStructure();
        if (File.Exists(PLAYER_DATA_PATH))
        {
            data = JsonUtility.FromJson<GetItemsResponseStructure>(File.ReadAllText(PLAYER_DATA_PATH));
        }

        var databaseItems = data.content.ToList();
        var levelItem = databaseItems.FirstOrDefault(x => x!=null && x.levelID == levelID);
        if (levelItem == null)
        {
            databaseItems.Add(new DatabaseItem(){levelID = levelID, minTurnsCount = turnsCount});
        }
        else
        {
            levelItem.minTurnsCount = turnsCount;
        }

        data.content = databaseItems.ToArray();
        File.WriteAllText(PLAYER_DATA_PATH, JsonUtility.ToJson(data));
        Debug.Log($"LevelHistory::Progress saved to {PLAYER_DATA_PATH}");
    }
}
