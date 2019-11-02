using System;
using UnityEngine;

    [Serializable]
    public struct ActiveState
    {
        public GameObject GameObject;
        public bool IsActive;
    }
    
    public class ToggleActiveStateOnEnableBehavior : MonoBehaviour
    {
        public ActiveState[] Objects;

        private void OnEnable()
        {
            foreach (var activeState in Objects)
            {
                activeState.GameObject.SetActive(activeState.IsActive);
                Debug.Log($"{name}:{GetType().Name}::Changed {activeState.GameObject.name} active state to {activeState.IsActive}");
            }
        }

        private void OnDisable()
        {
            foreach (var activeState in Objects)
            {
                if (activeState.GameObject != null)
                {
                    activeState.GameObject.SetActive(!activeState.IsActive);
                    Debug.Log($"{name}:{GetType().Name}::Changed {activeState.GameObject.name} active state to {!activeState.IsActive}");
                }  
            }
        }
    }