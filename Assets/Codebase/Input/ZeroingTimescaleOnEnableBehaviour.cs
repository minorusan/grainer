using UnityEngine;

namespace Codebase.Input
{
    public class ZeroingTimescaleOnEnableBehaviour : MonoBehaviour
    {
        private void OnEnable()
        {
            Time.timeScale = 0;
        }

        private void OnDisable()
        {
            Time.timeScale = 1;
        }
    }
}
