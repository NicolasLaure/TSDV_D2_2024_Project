using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSceneTrigger : MonoBehaviour
{
    //[SerializeField] string nextSceneName;
    private void OnTriggerEnter(Collider other)
    {
        Loader.ChangeToAsyncLoadedScene();
    }
}
