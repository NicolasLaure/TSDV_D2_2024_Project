using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Pause : MonoBehaviour
{
    public Action<bool> onPausePanelStateChange;
    private bool _shouldUnpause = true;

    private void OnEnable()
    {
        _shouldUnpause = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
        onPausePanelStateChange?.Invoke(true);
    }

    private void OnDisable()
    {
        if (_shouldUnpause)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
            onPausePanelStateChange?.Invoke(false);
        }
    }

    public void LoadMainMenu()
    {
        _shouldUnpause = false;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 1;

        Loader.ChangeScene("MainMenuEnvironment");
    }

    public void ReloadScene()
    {
        _shouldUnpause = false;
        Time.timeScale = 1;

        Loader.ReloadScene();
    }
}