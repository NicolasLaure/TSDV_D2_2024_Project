using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
public class Loader : MonoBehaviour
{
    [SerializeField] private List<string> scenesInBuildNames = new List<string>();
    private AsyncOperation changeSceneAsync = null;
    private int currentScene;

    private void Awake()
    {
        changeSceneAsync = null;
    }
    public static void ChangeScene(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex, LoadSceneMode.Single);
    }

    public static void ChangeScene(string name)
    {
        Scene nextScene = SceneManager.GetSceneByName(name);

        if (nextScene != null)
            SceneManager.LoadScene(name, LoadSceneMode.Single);
        else
            Debug.LogError($"There is no scene with {name} name in build");
    }

    public void AddScene(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex, LoadSceneMode.Additive);
    }

    public static void AddScene(string name)
    {
        Scene nextScene = SceneManager.GetSceneByName(name);

        if (nextScene != null)
            SceneManager.LoadScene(name, LoadSceneMode.Additive);
        else
            Debug.LogError($"There is no scene with {name} name in build");
    }
    public void LoadSceneAsync(string sceneName)
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        Debug.Log(sceneName);
        if (changeSceneAsync == null)
        {
            changeSceneAsync = SceneManager.LoadSceneAsync(sceneName);
            changeSceneAsync.allowSceneActivation = false;
        }
    }
    public void ChangeToAsyncLoadedScene()
    {
        changeSceneAsync.allowSceneActivation = true;
        RemoveScene(currentScene);
    }
    public void RemoveScene(int buildIndex)
    {
        SceneManager.UnloadSceneAsync(buildIndex);
    }

    public void ExitApp()
    {
        Application.Quit();
    }

    [ContextMenu("Populate List of names")]
    private void PopulateNames()
    {
        scenesInBuildNames.Clear();
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string name = System.IO.Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i));
            scenesInBuildNames.Add(name);
        }
    }
}
