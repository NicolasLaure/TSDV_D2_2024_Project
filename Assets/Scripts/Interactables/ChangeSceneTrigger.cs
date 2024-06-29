using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSceneTrigger : MonoBehaviour
{
    [SerializeField] string nextSceneName;
    [SerializeField] Loader loader;
    private void OnTriggerEnter(Collider other)
    {
        loader.ChangeToAsyncLoadedScene();
    }
}
