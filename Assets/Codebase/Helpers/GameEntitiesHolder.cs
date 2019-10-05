using System.Collections.Generic;
using UnityEngine;

public static class GameEntitiesHolder
{
    private static List<GameEntityBase> gameEntities = new List<GameEntityBase>();
    private static HashSet<GameEntityBase> gameEntitiesSet = new HashSet<GameEntityBase>();
    private static Dictionary<int, GameEntityBase> entitiesMap = new Dictionary<int, GameEntityBase>();

    public static int EntitiesCount => gameEntities.Count;
    public static GameEntityBase[] RegisteredEntites => gameEntities.ToArray();

    public static bool RegisterEntity(GameEntityBase entity)
    {
        if (gameEntitiesSet.Contains(entity))
        {
            return false;
        }
        else
        {
            gameEntitiesSet.Add(entity);
            gameEntities.Add(entity);
            entitiesMap.Add(entity.gameObject.GetInstanceID(), entity);
            return true;
        }
    }

    public static bool UnregisterEntity(GameEntityBase entity)
    {
        if (!gameEntitiesSet.Contains(entity))
        {
            return false;
        }
        else
        {
            gameEntitiesSet.Remove(entity);
            gameEntities.Remove(entity);
            entitiesMap.Remove(entity.gameObject.GetInstanceID());
            return true;
        }
    }

    public static bool GetEntity(GameObject obj, out GameEntityBase entity)
    {
        entity = null;
        if (entitiesMap.ContainsKey(obj.GetInstanceID()))
        {
            entity = entitiesMap[obj.GetInstanceID()];
            return true;
        }

        return false;
    }
}