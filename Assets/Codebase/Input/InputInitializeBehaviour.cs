using UnityEngine;

namespace Codebase.Input
{
    public class InputInitializeBehaviour : MonoBehaviour
    {
        public bool UseRecord;
        private void Start()
        {
#if !UNITY_EDITOR
            UseRecord = false;
#endif
            Instantiate(ResourceHelper.GetProvider(UseRecord), transform);
        }
    }
}