using System;
using System.IO;
using UnityEngine;

namespace Codebase.Input
{
    public abstract class InputProviderBase : MonoBehaviour
    {
        private InputHistory currentHistory = new InputHistory();
        public InputHistory GetLastHistory => currentHistory;
    
        public bool IsEnabled = true;
        public static event Action<InputChangedEventArgs> InputChanged = delegate(InputChangedEventArgs arg) { };
        public bool DumpOnDisable;

        private void Awake()
        {
            currentHistory.Inputs.Clear();
        }

        public void AddInput(MovementDirection direction, Vector3 position)
        {
            currentHistory.Inputs.Add(new InputRecord()
            {
                Direction = direction,
                InputPosition = position,
                Timestamp =  Time.timeSinceLevelLoad
            });
        }

        protected void InvokeEvent(MovementDirection direction)
        {
            if (IsEnabled)
            {
                InputChanged(new InputChangedEventArgs(direction, Time.timeSinceLevelLoad));
            }
        }

        private void OnDisable()
        {
#if UNITY_EDITOR
            if (DumpOnDisable)
            {
                File.WriteAllText("Assets/last_path.txt", JsonUtility.ToJson(currentHistory));   
            }  
#endif
        }
    }
}