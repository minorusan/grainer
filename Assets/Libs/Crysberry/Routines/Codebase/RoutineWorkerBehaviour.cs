using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Crysberry.Routines
{
    public class RoutineWorkerBehaviour : MonoBehaviour
    {
        public void StartRoutine(IEnumerator routine)
        {
            StartCoroutine(routine);
        }

        public void StopRoutines()
        {
            StopAllCoroutines();
        }
    }
}

