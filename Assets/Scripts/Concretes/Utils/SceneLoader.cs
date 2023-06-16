using System;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    public enum Scene
    {
        MenuScene,
        LoadingScene,
        GameScene
    }

    private static Scene _targetScene;

    public static async Task LoadSceneAsyncCustom(Scene targetScene, double loadingSceneDuration)
    {
        _targetScene = targetScene;
        SceneManager.LoadScene(Scene.LoadingScene.ToString()); // Load Loading Scene before any scene

        await Task.Delay(TimeSpan.FromSeconds(loadingSceneDuration)); // Delay for given seconds

        SceneManager.LoadSceneAsync(_targetScene.ToString());
    }
}
