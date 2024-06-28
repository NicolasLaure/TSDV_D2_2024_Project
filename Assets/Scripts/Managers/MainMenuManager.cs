using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameSaveSO saveFile;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void StartGame()
    {
        if (!saveFile.wasTutorialFinished)
            Loader.ChangeScene("TutorialScene");
        else
            Loader.ChangeScene("ShootingRange");
    }
}
