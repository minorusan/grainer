using System;
using System.Text;

[Serializable]
public class BaseRequest
{
    public string appKey = NetworkingConstants.APP_KEY;
}

[Serializable]
public class ModifyLevelRequest : BaseRequest
{
    public int levelID;
    public int minTurnsCount;
    public int version;
    
    public ModifyLevelRequest(int lvlID, int levelVersion, int newMinTurns)
    {
        this.levelID = lvlID;
        this.version = levelVersion;
        this.minTurnsCount = newMinTurns;
    }
}

[Serializable]
public class ResetDBRequest : BaseRequest
{
    public int lvlCount = 100;

    public ResetDBRequest(int lvlCount)
    {
        this.lvlCount = lvlCount;
    }
}

[Serializable]
public class ResponseBaseStructure
{
    public string message;
}

[Serializable]
public class LevelsDatabaseStructure : ResponseBaseStructure
{
    public DatabaseItem[] content;

    public LevelsDatabaseStructure()
    {
        content = new []{new DatabaseItem()};
    }
}

[Serializable]
public class DatabaseItem
{
    public string _id;
    public int levelID;
    public int version;
    public int minTurnsCount;

    public DatabaseItem()
    {
        levelID = 0;
        version = 1;
        minTurnsCount = 100;
    }

    public override string ToString()
    {
        return $"levelID:{levelID}, minTurnsCount:{minTurnsCount}";
    }
}

public static class NetworkingConstants
{
    public const string APP_KEY = "729yfd8as97sdf9yw7894gf7ygbdfs79e67789wyfgyds";
    public const string ROOT_URL = "https://gentle-coast-35203.herokuapp.com";
    public const string RESET_DB_ROUTE = "/api/seeddb/";
    public const string GET_ITEMS_ROUTE = "/api/getitems/";
    public const string UPDATE_ITEM_ROUTE = "/api/modifylvl/";
}