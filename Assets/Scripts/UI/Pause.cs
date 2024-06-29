using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Pause : MonoBehaviour
{
    public Action<bool> onPausePanelStateChange;
    bool shouldUnpause = true;
    private void OnEnable()
    {
        shouldUnpause = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
        onPausePanelStateChange?.Invoke(true);
    }
    private void OnDisable()
    {
        if (shouldUnpause)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
            onPausePanelStateChange?.Invoke(false);
        }
    }

    public void LoadMainMenu()
    {
        shouldUnpause = false;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 1;

        Loader.ChangeScene("MainMenuEnvironment");
    }
}
