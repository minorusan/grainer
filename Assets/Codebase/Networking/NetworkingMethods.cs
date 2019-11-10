using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkingMethods : MonoBehaviour
{
    public static IEnumerator Put<T>(string url, string postData, Action<string> success = null, Action<string> invalid = null, Action<T> responseDataCallback = null) where T:ResponseBaseStructure
    {
        byte[] bodyRaw = Encoding.UTF8.GetBytes(postData);
        using (UnityWebRequest www = UnityWebRequest.Put(url, bodyRaw))
        {
            www.SetRequestHeader("Content-Type", "application/json");
               
            var t = www.SendWebRequest();
            yield return t;
            if (t.webRequest.isDone && !www.isHttpError && www.responseCode == 200)
            {
                string responseJSON = Encoding.UTF8.GetString(www.downloadHandler.data, 0, www.downloadHandler.data.Length);
                var convertedJSON = JsonUtility.FromJson<T>(responseJSON);
                Debug.Log($"<color=blue> Networking::Message <{convertedJSON.message}></color>" );
                success?.Invoke(convertedJSON.message);
                responseDataCallback?.Invoke(convertedJSON);
            }

            if (www.isHttpError)
            {
                Debug.Log($"<color=red> Networking::Error {www.error}</color>");
                invalid?.Invoke(www.error);
            }
        }
    }
}
