using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using Crysberry.Audio;

public class AudioTests
{
    private const float titbitLonger = 1.1f; //Used to give a bit more time in duration checks due to Unity API
    
    [Test]
    public void TestResourcesExist()
    {
        var testSound = GetTestClip();
        var testAudioEffect = GetTestDefinition();
        
        Assert.IsTrue(testSound != null && testAudioEffect != null);
    }

    [UnityTest]
    public IEnumerator NormalPriorityAudioPlayersCountPerSecondDoesNotExceedConstraint()
    {
        var audioService = new AudioService();
        
        var testDefintion = GetTestDefinition();
        testDefintion.AudioClip = GetTestClip();
        
        audioService.SoundsPerSecond = 5;
        for (int i = 0; i < 10; i++)
        {
            audioService.Play(testDefintion);
        }
        
        yield return new WaitForEndOfFrame();

        var audioPlayers = GameObject.FindObjectsOfType<AudioPlayerBehavior>();
        
        Assert.LessOrEqual(audioPlayers.Length, audioService.SoundsPerSecond);
    }

    [UnityTest]
    public IEnumerator PlayerGainsMaxVolumeInGivenTimeWhenFadeInSettingIsSet()
    {
        //This guy can sometimes fail due to Unity's behaviour. Please, re-run failed several times to ensure issue
        var audioService = new AudioService();
        
        var testDefintion = GetTestDefinition();
        testDefintion.AudioClip = GetTestClip();
        var audioClipLength = testDefintion.AudioClip.length;
        
        testDefintion.Volume = 0.3f;
        testDefintion.FadesIn = true;
        testDefintion.FadeInEndPercentage = 0.5f;

        var player = audioService.Play(testDefintion);

        yield return new WaitForSeconds(audioClipLength * testDefintion.FadeInEndPercentage * titbitLonger); 
        
        Assert.GreaterOrEqual(player.GetComponent<AudioSource>().volume, testDefintion.Volume);
    }
    
    [UnityTest]
    public IEnumerator PlayerStartsMutedWhenFadeInSettingIsSet()
    {
        var audioService = new AudioService();
        
        var testDefintion = GetTestDefinition();
        testDefintion.AudioClip = GetTestClip();
        
        testDefintion.Volume = 0.3f;
        testDefintion.FadesIn = true;
        testDefintion.FadeInEndPercentage = 0.5f;

        var player = audioService.Play(testDefintion);
        
        yield return null;
        
        Assert.LessOrEqual(player.GetComponent<AudioSource>().volume, 0.1f);
    }
    
    [UnityTest]
    public IEnumerator HighPriorityAudioPlayersCountPerSecondCanExceedConstraint()
    {
        var audioService = new AudioService();
        
        var testDefintion = GetTestDefinition();
        testDefintion.AudioClip = GetTestClip();
        
        audioService.SoundsPerSecond = 5;
        
        for (int i = 0; i < 10; i++)
        {
            audioService.Play(testDefintion, AudioPriority.High);
        }
        
        yield return new WaitForEndOfFrame();

        var audioPlayers = GameObject.FindObjectsOfType<AudioPlayerBehavior>();
        
        Assert.GreaterOrEqual(audioPlayers.Length, audioService.SoundsPerSecond);
    }

    [UnityTest]
    public IEnumerator PlayerIsDestroyedIfAutokillSettingWasSet()
    {
        var audioService = new AudioService();
        var testDefintion = GetTestDefinition();
        testDefintion.AudioClip = GetTestClip();
        
        testDefintion.AutokillOnEnd = true;
        
        audioService.Play(testDefintion);
        
        yield return new WaitForSeconds(testDefintion.AudioClip.length * titbitLonger);
        
        Assert.LessOrEqual(GameObject.FindObjectsOfType<AudioPlayerBehavior>().Length, 0f);
    }

    [UnityTest]
    public IEnumerator PlayerEndsMutedWhenFadeOutSettingIsSet()
    {
        var audioService = new AudioService();
        
        var testDefintion = GetTestDefinition();
        testDefintion.AudioClip = GetTestClip();
        
        testDefintion.Volume = 0.3f;
        testDefintion.FadesOut = true;
        testDefintion.FadeOutBeginPercentage = 0.7f;

        var player = audioService.Play(testDefintion);
        
        yield return new WaitForSeconds(testDefintion.AudioClip.length * titbitLonger);
        
        Assert.LessOrEqual(player.GetComponent<AudioSource>().volume, 0.1f);
    }

    [TearDown]
    public void Clear()
    {
        var testDefinition = GetTestDefinition();
        testDefinition.FadesOut = false;
        testDefinition.FadesIn = false;
        testDefinition.AutokillOnEnd = false;
        
        var objects = GameObject.FindObjectsOfType<AudioPlayerBehavior>();
        for (int i = 0; i < objects.Length; i++)
        {
            GameObject.Destroy(objects[i].gameObject);
        }
    }

    private AudioClip GetTestClip()
    {
        return Resources.Load<AudioClip>("Crysberry/AudioTests/testClip");
    }

    private AudioEffectDefinition GetTestDefinition()
    {
        return Resources.Load<AudioEffectDefinition>("Crysberry/AudioTests/TestAudioEffect");
    }
}
