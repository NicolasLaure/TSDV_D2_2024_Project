using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    private static bool isFirstRun = true;

    [RuntimeInitializeOnLoadMethod]
    static void OnRuntimeMethodLoad()
    {
        isFirstRun = true;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void StartGame()
    {
        if (isFirstRun)
            Loader.ChangeScene(1);
        else
            Loader.ChangeScene(2);

        isFirstRun = false;
    }
}
