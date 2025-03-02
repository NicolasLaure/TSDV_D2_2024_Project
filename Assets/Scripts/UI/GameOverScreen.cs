using System;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void LoadMainMenu()
    {
        Loader.ChangeScene("MainMenuEnvironment");
    }

    public void ReloadScene()
    {
        Loader.ReloadScene();
    }
}