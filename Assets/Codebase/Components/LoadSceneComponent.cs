using System.Collections;
using Crysberry.Routines;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LoadSceneComponent : MonoBehaviour
{
    private string[] gameplaySceneNames = new[] {"gameplay", "gameplay_1"};
    public UnityEvent LoadStarted;

    public void LoadScene(string sceneName)
    {
#if UNITY_EDITOR
        var area = FindObjectOfType<AreaInitializeBehaviour>();
        if (area != null && area.DebugMode)
        {
            sceneName = SceneManager.GetActiveScene().name;
            LoadStarted.Invoke();
            Routiner.StartCouroutine(LoadYourAsyncScene(sceneName));
            return;
        }
#endif
       
        if (sceneName.Contains("gameplay"))
        {
            if (LevelsHistory.GamePlayLevelID >= LevelsHistory.GetMaxLevelNumber())
            {
                LoadScene("main_menu");
                return;
            }

            sceneName = "gameplay";
        }
        LoadStarted.Invoke();
        Routiner.StartCouroutine(LoadYourAsyncScene(sceneName));
    }
    
    IEnumerator LoadYourAsyncScene(string name)
    {
        // The Application loads the Scene in the background as the current Scene runs.
        // This is particularly good for creating loading screens.
        // You could also load the Scene by using sceneBuildIndex. In this case Scene2 has
        // a sceneBuildIndex of 1 as shown in Build Settings.

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(name);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}