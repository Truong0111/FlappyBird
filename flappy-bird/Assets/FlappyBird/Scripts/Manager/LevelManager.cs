using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Sequences;

public class LevelManager : Singleton<LevelManager>
{
    public SceneReference masterScene;
    public SceneReference[] levelScene;

    private AsyncOperation asyncOperation;
    

    public void StartGame()
    {
        StartCoroutine(LoadScene(levelScene[0]));
    }
    
    public void BackToMenu()
    {
        StartCoroutine(UnloadScene(levelScene[0], StartGame));
    }
    

    private IEnumerator LoadScene(SceneReference scenePath, Action action = null)
    {
        var sceneIndex = GetIndexSceneBuild(scenePath);

        if (sceneIndex == -1)
        {
            Debug.LogError("Scene not found");
            yield break;
        }

        asyncOperation = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);
        yield return new WaitUntil(() => asyncOperation.isDone);

        var scene = SceneManager.GetSceneByPath(scenePath);
        SceneManager.SetActiveScene(scene);
        
        action?.Invoke();
    }

    private IEnumerator UnloadScene(SceneReference scenePath, Action action = null)
    {
        var sceneIndex = GetIndexSceneBuild(scenePath);
        if (sceneIndex == -1)
        {
            Debug.LogError("Scene not found");
            yield break;
        }

        asyncOperation = SceneManager.UnloadSceneAsync(sceneIndex, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
        yield return new WaitUntil(() => asyncOperation.isDone);
        
        action?.Invoke();
    }

    private static int GetIndexSceneBuild(SceneReference scenePath)
    {
        var sceneCount = SceneManager.sceneCountInBuildSettings;

        var scene = SceneManager.GetSceneByPath(scenePath.path);

        for (var i = 0; i < sceneCount; i++)
        {
            var sceneCheck = SceneManager.GetSceneByBuildIndex(i);
            if (sceneCheck == scene) return i;
        }

        return -1;
    }
}