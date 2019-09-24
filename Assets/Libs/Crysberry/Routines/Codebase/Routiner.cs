using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crysberry.Routines
{
    public partial class Routiner
    {
        private const string instanceName = "Routiner";
        private static RoutineWorkerBehaviour _instance;

        public static void StartCouroutine(IEnumerator coroutine)
        {
            _instance.StartRoutine(coroutine);
        }
        
        public static void AwaitAsyncOperation<T>(Action<T> operationCallback, T operation) where T : AsyncOperation
        {
            CreateInstanceIfNeeded();
            _instance.StartRoutine(AwaitAsyncOperationRoutine(operationCallback, operation));
        }
        
        public static void InvokeContinuousWithDelays(Action method, float delay, Func<bool> exitCondition = null)
        {
            CreateInstanceIfNeeded();
            _instance.StartRoutine(InvokeEverySeveralSecondsRoutine(method, delay, exitCondition));
        }

        public static void InvokeOnFixedUpdate(Action method)
        {
            CreateInstanceIfNeeded();
            _instance.StartRoutine(InvokeFixedUpdateRoutine(method));
        }
        
        public static void InvokeEveryFrame(Action method, Func<bool> exitCondition = null)
        {
            CreateInstanceIfNeeded();
            _instance.StartRoutine(InvokeEveryFrameRoutine(method, exitCondition));
        }

        public static void InvokeNextFrame(Action method)
        {
            CreateInstanceIfNeeded();
            _instance.StartRoutine(InvokeNextFrameRoutine(method));
        }

        public static void InvokeDelayed(Action method, float delay)
        {
            CreateInstanceIfNeeded();
            _instance.StartRoutine(InvokeDelayedRoutine(method, delay));
        }

        public static void CancelAll()
        {
            CreateInstanceIfNeeded();
            _instance.StopRoutines();
        }

        private static void CreateInstanceIfNeeded()
        {
            if (_instance == null)
            {
                _instance = new GameObject(instanceName).AddComponent<RoutineWorkerBehaviour>();
                GameObject.DontDestroyOnLoad(_instance);
            }
        }
    }

}

