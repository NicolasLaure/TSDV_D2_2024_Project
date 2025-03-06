using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    [SerializeField] private List<string> scenesInBuildNames = new List<string>();
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

    public static void ChangeScene(int buildIndex)
    {
        if (SceneManager.loadedSceneCount > 1)
        {
            if (currentSceneIndex != 0)
                RemoveScene(currentSceneIndex);

            SceneManager.LoadScene(buildIndex, LoadSceneMode.Additive);
        }
        else
            SceneManager.LoadScene(buildIndex, LoadSceneMode.Single);

        currentSceneIndex = buildIndex;
    }

    public static void ChangeScene(string name)
    {
        if (SceneManager.loadedSceneCount > 1)
        {
            if (currentSceneIndex != 0)
                RemoveScene(currentSceneIndex);

            SceneManager.LoadScene(name, LoadSceneMode.Additive);
        }
        else
            SceneManager.LoadScene(name, LoadSceneMode.Single);

        currentSceneIndex = SceneManager.GetSceneByName(name).buildIndex;
    }

    public static void ReloadScene()
    {
        if (SceneManager.loadedSceneCount > 1)
        {
            if (currentSceneIndex != 0)
                RemoveScene(currentSceneIndex);

            SceneManager.LoadScene(currentSceneIndex, LoadSceneMode.Additive);
        }
        else
            SceneManager.LoadScene(GetCurrentScene(), LoadSceneMode.Single);
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

    private static int GetCurrentScene()
    {
        return SceneManager.GetActiveScene().buildIndex;
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
            string name = Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i));
            scenesInBuildNames.Add(name);
        }
    }
}