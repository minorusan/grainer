using System;
using System.Collections;
using System.Collections.Generic;
using Crysberry.Routines;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Random = UnityEngine.Random;

public class RoutineTests
{
    private const string testObjectName = "test_object";
    
    [UnityTest]
    public IEnumerator RoutineInstanceIsCreatedOnDemand()
    {
        Routiner.InvokeNextFrame(delegate {  });
        yield return new WaitForEndOfFrame();
        Assert.NotNull(GameObject.FindObjectOfType<RoutineWorkerBehaviour>());
    }
    
    [UnityTest]
    public IEnumerator RoutineInstanceInvokesActionNextFrame()
    {
        Routiner.InvokeNextFrame(delegate { new GameObject(testObjectName).AddComponent<BoxCollider>();; });
        yield return new WaitForEndOfFrame();
        Assert.NotNull(GameObject.Find(testObjectName));
    }
    
    [UnityTest]
    public IEnumerator RoutineInstanceInvokesActionAfterDelay()
    {
        var delay = Random.Range(0.1f, 0.5f);
        Routiner.InvokeDelayed(delegate { new GameObject(testObjectName).AddComponent<BoxCollider>();; }, delay);
        yield return new WaitForSeconds(delay);
        Assert.NotNull(GameObject.Find(testObjectName));
    }
    
    [UnityTest]
    public IEnumerator RoutineInstanceInvokesActionEveryFrameAndQuitsWithCondition()
    {
        var parent = new GameObject("parent").AddComponent<BoxCollider>();
        var childCount = Random.Range(1, 5);
        var condition = new Func<bool>(() => parent.transform.childCount != childCount);
        
        Routiner.InvokeEveryFrame(delegate
        {
            var instance = new GameObject(testObjectName).AddComponent<BoxCollider>();
            instance.transform.SetParent(parent.transform);
            
        }, condition);

        for (int i = 0; i < childCount + 2; i++)
        {
            yield return new WaitForEndOfFrame();
        }

        Assert.AreEqual(childCount, parent.transform.childCount);
    }
    
    [UnityTest]
    public IEnumerator RoutineInstanceInvokesActionContinuouslyWithDelayAndQuitsWithCondition()
    {
        var parent = new GameObject("parent").AddComponent<BoxCollider>();
        var childCount = Random.Range(1, 5);
        var delay = Random.Range(0.1f, 0.3f);
        var condition = new Func<bool>(() => parent.transform.childCount != childCount);
        
        Routiner.InvokeContinuousWithDelays(delegate
        {
            var instance = new GameObject(testObjectName).AddComponent<BoxCollider>();
            instance.transform.SetParent(parent.transform);
            
        }, delay, condition);

        yield return new WaitForSeconds((delay * childCount)* 1.1f);

        Assert.AreEqual(childCount, parent.transform.childCount);
    }

    [UnityTest]
    public IEnumerator RoutinerStopsRoutines()
    {
        var delay = Random.Range(0.3f, 1f);
        Routiner.InvokeDelayed(() =>
        {
            new GameObject(testObjectName).AddComponent<BoxCollider>();
        }, delay);
        
        yield return new WaitForSeconds(delay * 0.5f);
        Routiner.CancelAll();
        yield return new WaitForSeconds(delay * 0.5f);
        Assert.IsNull(GameObject.FindObjectOfType<BoxCollider>());
    }

    [TearDown]
    public void Clear()
    {
        var objects = GameObject.FindObjectsOfType<BoxCollider>();
        foreach (var boxCollider in objects)
        {
            GameObject.Destroy(boxCollider.gameObject);
        }
    }
}