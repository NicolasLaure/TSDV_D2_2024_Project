using System;
using UnityEditor;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void LoadMainMenu()
    {
        Loader.ChangeScene("MainMenuEnvironment");
    }

    public void ReloadScene()
    {
        Loader.ReloadScene();
    }

    public void NextLevel()
    {
        Debug.Log("Next Scene");
        Loader.ChangeToNextScene();
    }
}