using System.IO;
using UnityEngine;

namespace Codebase.Input
{
    public class RecordInputProvisionBehaviour : InputProviderBase
    {
        private int currentIndex;
        private InputHistory currentHistory;
        public string InputRecordPath;

        private void Start()
        {
            currentHistory = JsonUtility.FromJson<InputHistory>(File.ReadAllText(InputRecordPath));
            if (currentHistory != null)
            {
                Debug.Log("Parsed level history");
            }
        }

        private void Update()
        {
            if (currentHistory != null && currentIndex < currentHistory.Inputs.Count)
            {
                var currentItem = currentHistory.Inputs[currentIndex];
                if (Time.timeSinceLevelLoad >= currentItem.Timestamp)
                {
                    InvokeEvent(currentItem.Direction);
                    currentIndex++;
                }
            }
        
        }
    }
}