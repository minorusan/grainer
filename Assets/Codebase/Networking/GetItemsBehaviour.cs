using System.Text;
using UnityEngine;

public class GetItemsBehaviour : MonoBehaviour
{
    private void Start()
    {
        Networking.GetItems((DatabaseItem[] docs) =>
        {
            var stringBuilder = new StringBuilder();
            foreach (var databaseItem in docs)
            {
                stringBuilder.Append($"{databaseItem.ToString()} \n");
            }
            Debug.Log(stringBuilder);
        }, s => Debug.LogError("Cannot get items :(") );
    }
}