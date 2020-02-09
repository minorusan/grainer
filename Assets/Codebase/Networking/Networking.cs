using System;
using Crysberry.Routines;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


public static class Networking
{
#if UNITY_EDITOR
    [MenuItem("Grainer/Reset database")]
#endif
    public static void ResetDatabase()
    {
        var method = NetworkingMethods.Put<ResponseBaseStructure>($"{NetworkingConstants.ROOT_URL}{NetworkingConstants.RESET_DB_ROUTE}",
            JsonUtility.ToJson(new ResetDBRequest(100)), s => Debug.Log(s), s => Debug.LogError(s));
        Routiner.StartCouroutine(method);
    }

    public static void GetItems(Action<DatabaseItem[]> documentsCallback, Action<string> failure)
    {
        var method = NetworkingMethods.Put<LevelsDatabaseStructure>($"{NetworkingConstants.ROOT_URL}{NetworkingConstants.GET_ITEMS_ROUTE}",
            JsonUtility.ToJson(new ResetDBRequest(100)), null, failure,
            o => documentsCallback(o.content));
        Routiner.StartCouroutine(method);
    }

    public static void UpdateItem(int levelID, int newMinTurns, Action<string> succes, Action<string> failure)
    {
        var method = NetworkingMethods.Put<LevelsDatabaseStructure>($"{NetworkingConstants.ROOT_URL}{NetworkingConstants.UPDATE_ITEM_ROUTE}",
            JsonUtility.ToJson(new ModifyLevelRequest(levelID, newMinTurns)), succes, failure,
            null);
        Routiner.StartCouroutine(method);
    }
}