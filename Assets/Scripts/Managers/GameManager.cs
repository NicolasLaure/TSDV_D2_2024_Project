using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int firstSceneIndex = 1;
    private void Start()
    {
        Loader.AddScene(firstSceneIndex);
        Loader.FirstInteractableSceneIndex = firstSceneIndex;
    }
}
