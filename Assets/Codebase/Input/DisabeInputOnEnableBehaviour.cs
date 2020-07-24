using UnityEngine;

namespace Codebase.Input
{
    public class DisabeInputOnEnableBehaviour : MonoBehaviour
    {
        private void OnEnable()
        {
            var provider = FindObjectOfType<InputProviderBase>();
            if (provider != null)
            {
                provider.IsEnabled = false;
            }
        }

        private void OnDisable()
        {
            var provider = FindObjectOfType<InputProviderBase>();
            if (provider != null)
            {
                provider.IsEnabled = true;
            }
        }
    }
}