using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
public class Loader : MonoBehaviour
{
    [SerializeField] private List<string> scenesInBuildNames = new List<string>();
    private static AsyncOperation changeSceneAsync = null;
    private static string asyncSceneName = null;
    private static int currentSceneIndex = 0;
    private static int firstInteractableSceneIndex = 0;
    private static int lastInteractableSceneIndex = 0;

    public static int FirstInteractableSceneIndex
    {
        get { return firstInteractableSceneIndex; }
        set
        {
            firstInteractableSceneIndex = value;
            lastInteractableSceneIndex = SceneManager.sceneCountInBuildSettings - firstInteractableSceneIndex;
        }
    }

    private void Awake()
    {
        changeSceneAsync = null;
    }
    public static void ChangeScene(int buildIndex)
    {
        if (currentSceneIndex != 0)
            RemoveScene(currentSceneIndex);

        SceneManager.LoadScene(buildIndex, LoadSceneMode.Additive);
        currentSceneIndex = buildIndex;
    }

    public static void ChangeScene(string name)
    {
        if (currentSceneIndex != 0)
            RemoveScene(currentSceneIndex);

        SceneManager.LoadScene(name, LoadSceneMode.Additive);
        currentSceneIndex = SceneManager.GetSceneByName(name).buildIndex;
    }
    public static void ChangeToNextScene()
    {
        if (currentSceneIndex + 1 <= lastInteractableSceneIndex)
            ChangeScene(currentSceneIndex + 1);
    }
    public static void ChangeToPreviousScene()
    {
        if (currentSceneIndex - 1 >= firstInteractableSceneIndex)
            ChangeScene(currentSceneIndex - 1);
    }
    public static void AddScene(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex, LoadSceneMode.Additive);
        currentSceneIndex = buildIndex;
    }

    public static void AddScene(string name)
    {
        SceneManager.LoadScene(name, LoadSceneMode.Additive);
        currentSceneIndex = SceneManager.GetSceneByName(name).buildIndex;
    }
    public static void LoadSceneAsync(string sceneName)
    {
        asyncSceneName = sceneName;
        if (changeSceneAsync == null)
        {
            changeSceneAsync = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            changeSceneAsync.allowSceneActivation = false;
        }
    }
    public static void ChangeToAsyncLoadedScene()
    {
        RemoveScene(currentSceneIndex);
        changeSceneAsync.allowSceneActivation = true;
        currentSceneIndex = SceneManager.GetSceneByName(asyncSceneName).buildIndex;
        changeSceneAsync = null;
    }
    public static void RemoveScene(int buildIndex)
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
