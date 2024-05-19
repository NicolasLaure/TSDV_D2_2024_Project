using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Pause : MonoBehaviour
{
    public Action<bool> onPausePanelStateChange;

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
        onPausePanelStateChange.Invoke(true);
    }
    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        onPausePanelStateChange.Invoke(false);
    }
}
