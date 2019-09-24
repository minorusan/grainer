using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crysberry.Routines
{
    public partial class Routiner
    {
        private static IEnumerator AwaitAsyncOperationRoutine<T>(Action<T> action, T operation) where T : AsyncOperation
        {
            while (!operation.isDone)
            {
                yield return 0;
            }

            action(operation);
        }
        
        private static IEnumerator InvokeEveryFrameRoutine(Action action, Func<bool> exitCondition = null)
        {
            var endOfFrame = new WaitForEndOfFrame();
            while (exitCondition == null || exitCondition())
            {
                yield return endOfFrame;
                action();
            }
        }

        private static IEnumerator InvokeFixedUpdateRoutine(Action action)
        {
            yield return new WaitForFixedUpdate();
            action();
        }
        
        private static IEnumerator InvokeEverySeveralSecondsRoutine(Action action, float delay,Func<bool> exitCondition = null)
        {
            var waitDelay = new WaitForSeconds(delay);
            while (exitCondition == null || exitCondition())
            {
                yield return waitDelay;
                action();
            }
        }
        
        private static IEnumerator InvokeNextFrameRoutine(Action action)
        {
            yield return new WaitForEndOfFrame();
            action();
        }

        private static IEnumerator InvokeDelayedRoutine(Action action, float delay)
        {
            yield return new WaitForSeconds(delay);
            action();
        }
    }
}

