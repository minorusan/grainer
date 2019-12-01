using UnityEngine;
using UnityEngine.SceneManagement;

public static class GameplayTimescale
{
    public static bool GameActive;

    static GameplayTimescale()
    {
        GameActive = true;
        SceneManager.sceneLoaded += SceneManagerOnSceneLoaded;
    }

    private static void SceneManagerOnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        GameActive = true;
        Time.timeScale = 1f;
    }
}
