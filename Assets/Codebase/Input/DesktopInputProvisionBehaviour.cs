using System.Collections.Generic;
using UnityEngine;

namespace Codebase.Input
{
    public class DesktopInputProvisionBehaviour : InputProviderBase
    {
        private List<MovementDirection> codesLastFrame = new List<MovementDirection>(4);
        public KeyCode Up, Down, Left, Right;

        private void Update()
        {
            var direction = GetMovementDirection();
            if (direction != MovementDirection.None)
            {
                InvokeEvent(direction);
            }
        }

        private MovementDirection GetMovementDirection()
        {
            if (UnityEngine.Input.GetKeyDown(Up))
            {
                return MovementDirection.Up;
            }

            if (UnityEngine.Input.GetKeyDown(Down))
            {
                return MovementDirection.Down;
            }

            if (UnityEngine.Input.GetKeyDown(Left))
            {
                return MovementDirection.Left;
            }

            if (UnityEngine.Input.GetKeyDown(Right))
            {
                return MovementDirection.Right;
            }

            return MovementDirection.None;
        }
    }
}