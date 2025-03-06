using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderCallsHandler : MonoBehaviour
{
    public void ChangeScene(int buildIndex)
    {
        Loader.ChangeScene(buildIndex);
    }

    public void ChangeScene(string name)
    {
        Loader.ChangeScene(name);
    }

    public void AddScene(int buildIndex)
    {
        Loader.AddScene(buildIndex);
    }

    public void AddScene(string name)
    {
        Loader.AddScene(name);
    }
    public void RemoveScene(int buildIndex)
    {
        Loader.RemoveScene(buildIndex);
    }
}
