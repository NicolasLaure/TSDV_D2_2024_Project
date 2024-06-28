using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Loader loader;
    private void Start()
    {
        loader.AddScene(1);
    }
}
